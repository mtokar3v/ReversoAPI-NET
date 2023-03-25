using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Shared.Infrastructure.Http.Interfaces
{
    public interface IHttpClient
    {
        Task<HttpResponse> GetAsync(Uri uri, CancellationToken cancellationToken = default);
        Task<HttpResponse> PostAsync(Uri uri, object data, CancellationToken cancellationToken = default);
    }
}