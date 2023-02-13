using HtmlAgilityPack;

namespace ReversoAPI.Web.Tools.Parsers
{
    public abstract class BaseResponseParser<TResult> : IResponseParser<TResult>
    {
        protected readonly HtmlDocument _html;

        public BaseResponseParser(string html)
        {
            _html = new HtmlDocument();
            _html.LoadHtml(html);
        }

        public abstract TResult Invoke();
    }
}
