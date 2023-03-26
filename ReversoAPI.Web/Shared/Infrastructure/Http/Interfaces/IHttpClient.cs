using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web
{
    public interface IHttpClient
    {
        Task<HttpResponse> GetAsync(Uri uri, CancellationToken cancellationToken = default);
        Task<HttpResponse> PostAsync(Uri uri, object data, CancellationToken cancellationToken = default);
    }
}