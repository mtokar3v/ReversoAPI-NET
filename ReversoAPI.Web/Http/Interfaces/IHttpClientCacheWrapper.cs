using System.Net.Http;

namespace ReversoAPI.Web.Http.Interfaces
{
    public interface IHttpClientCacheWrapper
    {
        HttpClient GetHttpClient();
    }
}