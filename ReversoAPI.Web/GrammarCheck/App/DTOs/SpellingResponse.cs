using System;
using System.Collections.Generic;

namespace ReversoAPI.Web.GrammarCheck.App.DTOs
{
    public class SpellingResponse
    {
        public Guid Id { get; set; }
        public string Engine { get; set; }
        public string Language { get; set; }
        public IEnumerable<CorrectionDto> Corrections { get; set; }
        public IEnumerable<SentenceDto> Sentences { get; set; }
        public StatsDto Stats { get; set; }
        public string Text { get; set; }
        public int TimeTaken { get; set; }
        public bool Truncated { get; set; }

    }
}
