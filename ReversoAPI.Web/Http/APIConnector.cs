using Polly;
using ReversoAPI.Web.Http.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            using var response = await Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => _httpStatusCodesWorthRetrying.Contains(r.StatusCode))
                .WaitAndRetryAsync(RetryAttemptCount, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)))
                .ExecuteAsync(() => _httpClient.GetAsync(uri));

            var content = await response.Content.ReadAsStringAsync();
            return new HttpResponse { Content = content };
        }
    }
}
