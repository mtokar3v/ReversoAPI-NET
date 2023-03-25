using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.Shared.Infrastructure.Http.Interfaces;

namespace ReversoAPI.Web.Shared.Infrastructure.Http
{
    public class CachedHttpClient : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public CachedHttpClient()
        {
            _httpClient = HttpClientCacheWrapper
                .GetInstance()
                .GetHttpClient();
        }

        public async Task<HttpResponse> GetAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            var response = await _httpClient
                .GetAsync(uri, cancellationToken)
                .ConfigureAwait(false);

            var content = await response.Content
                .ReadAsStreamAsync()
                .ConfigureAwait(false);

            return new HttpResponse(
                contentType: response.Content.Headers.ContentType.MediaType,
                content: await CopyAsync(content, cancellationToken).ConfigureAwait(false),
                response.StatusCode);
        }

        public async Task<HttpResponse> PostAsync(Uri uri, object payload, CancellationToken cancellationToken = default)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            var json = JsonConvert.SerializeObject(payload);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, data, cancellationToken);

            var content = await response.Content
                .ReadAsStreamAsync()
                .ConfigureAwait(false);

            return new HttpResponse(
                contentType: response.Content.Headers.ContentType.MediaType,
                content: await CopyAsync(content, cancellationToken).ConfigureAwait(false),
                response.StatusCode);
        }

        // TODO: Rid of this. HttpResponseMessage disposing leads to close Content stream.
        private async Task<Stream> CopyAsync(Stream stream, CancellationToken cancellationToken = default)
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
