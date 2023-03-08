using HtmlAgilityPack;
using ReversoAPI.Web.Shared.Domain.Interfaces.Services;
using System;
using System.IO;

namespace ReversoAPI.Web.Shared.Domain.Services
{
    public abstract class BaseParser<TResult> : IParser<TResult>
    {
        public TResult Invoke(Stream html)
        {
            if (html == null) throw new ArgumentNullException(nameof(html));

            var htmlDoc = new HtmlDocument();
            htmlDoc.Load(html);

            return Parse(htmlDoc);
        }

        protected abstract TResult Parse(HtmlDocument html);
    }
}
