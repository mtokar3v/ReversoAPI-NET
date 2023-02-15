using System;
using System.Net.Http;

namespace ReversoAPI.Web.Http
{
    public class HttpClientCacheWrapper
    {
        private const string RandomUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/110.0.0.0 Safari/537.36";
        private const bool DefaultCacheKey = true;

        private static HttpClientCacheWrapper _instance;

        private static object _instLock = new object();
        private static object _clntLock = new object();

        private ICache<bool, HttpClient> _cache;

        private HttpClientCacheWrapper()
        {
            _cache = new SimpleCache<bool, HttpClient>();
        }

        public static HttpClientCacheWrapper GetInstance()
        {
            if (_instance == null)
            {
                lock (_instLock)
                {
                    if (_instance == null)
                    {
                        _instance = new HttpClientCacheWrapper();
                    }
                }
            }

            return _instance;
        }

        public HttpClient GetHttpClient()
        {
            // I suppose it necessary to lock here to avoid point when two httpClient instance will are being created
            // and one of their will lost, but HttpClient IDisposable, so it may lead to memory leak
            lock (_clntLock)
            {
                return _cache.GetOrAdd(DefaultCacheKey, CreateHttpClient);
            }
        }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(15),
            };

            // User-agent is necessary to send a valid request to Reverso
            // But this realisation like string with random user-agent seems unsuccessful
            client.DefaultRequestHeaders.Add("user-agent", RandomUserAgent);
            client.DefaultRequestHeaders.Add("accept", "*/*");

            return client;
        }
    }
}
