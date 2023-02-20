using System.IO;

namespace ReversoAPI.Web.Tools.Parsers
{
    public interface IResponseParser<TResult>
    {
        TResult Invoke(Stream html);
    }
}
