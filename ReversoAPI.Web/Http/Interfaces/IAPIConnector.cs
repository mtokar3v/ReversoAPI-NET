using System;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Http.Interfaces
{
    public interface IAPIConnector
    {
        Task<T> GetAsync<T>(Uri uri);
    }
}