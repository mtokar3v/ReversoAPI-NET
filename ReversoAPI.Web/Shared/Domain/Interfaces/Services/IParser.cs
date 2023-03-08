using System.IO;

namespace ReversoAPI.Web.Shared.Domain.Interfaces.Services
{
    public interface IParser<TResult>
    {
        TResult Invoke(Stream html);
    }
}
