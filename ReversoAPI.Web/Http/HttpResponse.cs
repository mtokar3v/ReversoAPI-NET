namespace ReversoAPI.Web.Http
{
    public class HttpResponse
    { 
        public string Content { get; set; }

        public bool IsHtml()
        {
            if (!string.IsNullOrWhiteSpace(Content))
            {
                var trimmedText = Content.Trim();
                return trimmedText.StartsWith('<') && trimmedText.EndsWith('>');
            }

            return false;
        }
    }
}
