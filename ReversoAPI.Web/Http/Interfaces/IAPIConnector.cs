using System;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Http.Interfaces
{
    public interface IAPIConnector
    {
        Task<string> GetAsync(Uri uri);
    }
}