using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Newtonsoft.Json;
using ReversoAPI.Web.Shared.Infrastructure.Http.Interfaces;

namespace ReversoAPI.Web.Shared.Infrastructure.Http
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

        public async Task<HttpResponse> GetAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            using var response = await Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => _httpStatusCodesWorthRetrying.Contains(r.StatusCode))
                .WaitAndRetryAsync(RetryAttemptCount, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)))
                .ExecuteAsync(() => _httpClient.GetAsync(uri, cancellationToken))
                .ConfigureAwait(false);

            var content = await response.Content
                    .ReadAsStreamAsync()
                    .ConfigureAwait(false);

            return new HttpResponse
            {
                ContentType = response.Content.Headers.ContentType.MediaType,
                Content = await MakeACopyAsync(content, cancellationToken).ConfigureAwait(false),
            };
        }

        public async Task<HttpResponse> PostAsync(Uri uri, object payload, CancellationToken cancellationToken = default)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            if (payload == null) throw new ArgumentNullException(nameof(payload));

            var json = JsonConvert.SerializeObject(payload);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = await Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => _httpStatusCodesWorthRetrying.Contains(r.StatusCode))
                .WaitAndRetryAsync(RetryAttemptCount, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)))
                .ExecuteAsync(() => _httpClient.PostAsync(uri, data, cancellationToken))
                .ConfigureAwait(false);

            var content = await response.Content
                .ReadAsStreamAsync()
                .ConfigureAwait(false);

            return new HttpResponse
            {
                ContentType = response.Content.Headers.ContentType.MediaType,
                Content = await MakeACopyAsync(content, cancellationToken).ConfigureAwait(false),
            };
        }

        // TODO: Rid of this. HttpResponseMessage disposing leads to close Content stream.
        private async Task<Stream> MakeACopyAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            var ms = new MemoryStream();

            await stream
                .CopyToAsync(ms, cancellationToken)
                .ConfigureAwait(false);

            ms.Position = 0;
            return ms;
        }
    }
}
