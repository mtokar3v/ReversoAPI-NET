using System;
using System.IO;

namespace ReversoAPI.Web.Shared.Infrastructure.Http
{
    public class HttpResponse : IDisposable
    {
        public string ContentType { get; set; }
        public Stream Content { get; set; }

        public void Dispose()
        {
            Content.Dispose();
        }

        public bool IsHtml() => ContentType.Contains("text/html", StringComparison.CurrentCultureIgnoreCase);
    }
}
