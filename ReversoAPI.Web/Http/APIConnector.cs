using ReversoAPI.Web.Factories;
using ReversoAPI.Web.Http.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Http
{
    public class APIConnector : IAPIConnector
    {
        private const string RandomUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/110.0.0.0 Safari/537.36";
        // Bad solution, cause sockes are shuted down after long time
        public async Task<T> GetAsync<T>(Uri uri)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("user-agent", RandomUserAgent);
            httpClient.DefaultRequestHeaders.Add("accept", "*/*");

            //do retry
            var httpResponseMessage = await httpClient.GetAsync(uri);
            if (!httpResponseMessage.IsSuccessStatusCode) throw new HttpRequestException($"'GET {uri}' is failed");

            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            return ParserFactory.Create<T>(content).Invoke();
        }
    }
}
