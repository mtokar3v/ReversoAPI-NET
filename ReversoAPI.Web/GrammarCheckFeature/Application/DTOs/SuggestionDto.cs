namespace ReversoAPI.Web.GrammarCheckFeature.Application.DTOs
{
    public class SuggestionDto
    {
        public string Text { get; set; }
        public string Definition { get; set; }
        public string Calegory { get; set; } // Update to enum in future
    }
}
