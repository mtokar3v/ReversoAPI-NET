using System;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Http.Interfaces
{
    public interface IAPIConnector
    {
        Task<HttpResponse> GetAsync(Uri uri);
    }
}