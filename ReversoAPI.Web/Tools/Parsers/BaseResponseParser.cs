using HtmlAgilityPack;
using System;
using System.IO;

namespace ReversoAPI.Web.Tools.Parsers
{
    public abstract class BaseResponseParser<TResult> : IResponseParser<TResult>
    {
        public TResult Invoke(string html)
        {
            if (string.IsNullOrEmpty(html)) throw new ArgumentNullException(nameof(html));

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            return Parse(htmlDoc);
        }

        protected abstract TResult Parse(HtmlDocument html);
    }
}
