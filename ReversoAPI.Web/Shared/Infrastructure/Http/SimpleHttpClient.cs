using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Shared.Infrastructure.Http
{
    public class SimpleHttpClient : IHttpClient
    {
        private const string RandomUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36";

        private readonly HttpClient _httpClient;

        public SimpleHttpClient()
        {
            _httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(15),
            };

            // User-agent is necessary to send a valid request to Reverso
            // But this realisation like string with random user-agent seems unsuccessful
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(RandomUserAgent);
            _httpClient.DefaultRequestHeaders.Accept.ParseAdd("*/*");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
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

        public void Dispose()
        {
            _httpClient?.Dispose();
            GC.SuppressFinalize(this);
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
