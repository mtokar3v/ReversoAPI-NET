using System.Collections.Generic;

namespace ReversoAPI.Web.GrammarCheckFeature.Application.DTOs
{
    public class CorrectionDto
    {
        public string CorrectionText { get; set; }
        public string MistakeText { get; set; }

        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        public string Group { get; set; } // Update to enum in future
        public IEnumerable<SuggestionDto> Suggestions { get; set; }
    }
}
