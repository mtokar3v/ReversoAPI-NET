using System;
using System.IO;
using System.Net;

namespace ReversoAPI.Web
{
    public class HttpResponse : IDisposable
    {
        public HttpResponse(string contentType, Stream content, HttpStatusCode statusCode)
        {
            ContentType = contentType;
            Content = content;
            StatusCode = statusCode;
        }

        public string ContentType { get; }
        public Stream Content { get; }
        public HttpStatusCode StatusCode { get; }

        public void Dispose()
        {
            Content.Dispose();
        }

        public bool IsHtml() => ContentType.Contains("text/html", StringComparison.CurrentCultureIgnoreCase);
    }
}
