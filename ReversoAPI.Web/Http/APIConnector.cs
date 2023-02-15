using ReversoAPI.Web.Http.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Http
{
    public class APIConnector : IAPIConnector
    {
        private readonly HttpClient _httpClient;

        public APIConnector()
        {
            _httpClient = HttpClientCacheWrapper
                .GetInstance()
                .GetHttpClient();
        }

        public async Task<string> GetAsync(Uri uri)
        {
            //TO DO: Add do retry logic
            using var httpResponseMessage = await _httpClient.GetAsync(uri);
            if (!httpResponseMessage.IsSuccessStatusCode) throw new HttpRequestException($"'GET {uri}' is failed");

            // Why does APIConnector GET return string, not something like a HttpResponse?
            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
    }
}
