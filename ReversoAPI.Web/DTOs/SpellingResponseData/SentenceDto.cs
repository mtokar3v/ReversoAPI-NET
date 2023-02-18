namespace ReversoAPI.Web.DTOs.SpellingResponseData
{
    public class SentenceDto
    {
        public int EndIndex { get; set; }
        public int StartIndex { get; set; }
        public string Status { get; set; } // Update to enum in future
    }
}
