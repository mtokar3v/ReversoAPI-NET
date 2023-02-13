using ReversoAPI.Web.Models.Responses;
using ReversoAPI.Web.Tools.Parsers;

namespace ReversoAPI.Web.Factories
{
    public class ParserFactory
    {
        public static IResponseParser<T> Create<T>(string html)
        {
            if(typeof(T) == typeof(ContextResponse)) return new ContextResponseParser(html) as IResponseParser<T>;
            if(typeof(T) == typeof(SynonymsResponse)) return new SynonymsResponseParser(html) as IResponseParser<T>;

            return null;
        }
    }
}
