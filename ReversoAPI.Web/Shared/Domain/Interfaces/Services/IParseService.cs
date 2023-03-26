using System.IO;

namespace ReversoAPI.Web
{
    public interface IParseService<TResult>
    {
        TResult Invoke(Stream html);
    }
}
