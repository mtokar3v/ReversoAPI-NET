using System;
using System.IO;
using ReversoAPI.Web.Shared.Domain.Interfaces.Services;

namespace ReversoAPI.Web.Shared.Domain.Services
{
    public abstract class BaseParser<TResult> : IParseService<TResult>
    {
        public TResult Invoke(Stream htmlStream)
        {
            if (htmlStream == null) throw new ArgumentNullException(nameof(htmlStream));
            return Parse(htmlStream);
        }

        protected abstract TResult Parse(Stream htmlStream);
    }
}
