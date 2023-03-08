using System.Net.Http;

namespace ReversoAPI.Web.Shared.Infrastructure.Http.Interfaces
{
    public interface IHttpClientCacheWrapper
    {
        HttpClient GetHttpClient();
    }
}