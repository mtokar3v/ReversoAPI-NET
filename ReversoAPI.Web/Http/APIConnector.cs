using Newtonsoft.Json;
using Polly;
using ReversoAPI.Web.Http.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Http
{
    public class APIConnector : IAPIConnector
    {
        private readonly HttpClient _httpClient;

        private const int RetryAttemptCount = 4;
        private static readonly HttpStatusCode[] _httpStatusCodesWorthRetrying = 
        {
           HttpStatusCode.RequestTimeout, 
           HttpStatusCode.InternalServerError,
           HttpStatusCode.BadGateway, 
           HttpStatusCode.ServiceUnavailable, 
           HttpStatusCode.GatewayTimeout
        };

        private APIConnector(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public static APIConnector Create(IHttpClientCacheWrapper httpClientCache)
        {
            return new APIConnector(httpClientCache.GetHttpClient());
        }

        public async Task<HttpResponse> GetAsync(Uri uri)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            using var response = await Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => _httpStatusCodesWorthRetrying.Contains(r.StatusCode))
                .WaitAndRetryAsync(RetryAttemptCount, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)))
                .ExecuteAsync(() => _httpClient.GetAsync(uri));

            var content = await response.Content.ReadAsStringAsync();
            return new HttpResponse { Content = content };
        }

        public async Task<HttpResponse> PostAsync(Uri uri, object payload)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            if (payload == null) throw new ArgumentNullException(nameof(payload));

            var json = JsonConvert.SerializeObject(payload);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, data);
            var content = await response.Content.ReadAsStringAsync();
            return new HttpResponse { Content = content };
        }
    }
}
