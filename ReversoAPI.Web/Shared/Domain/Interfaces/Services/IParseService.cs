using System.IO;

namespace ReversoAPI.Web.Shared.Domain.Interfaces.Services
{
    public interface IParseService<TResult>
    {
        TResult Invoke(Stream html);
    }
}
