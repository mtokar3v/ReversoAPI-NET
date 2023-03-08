using System;
using System.Collections.Generic;

namespace ReversoAPI.Web.Translation.App.DTOs
{
    public class TranslationResponse
    {
        public Guid Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public IEnumerable<string> Input { get; set; }
        public string CorrectedText { get; set; }
        public IEnumerable<string> Translation { get; set; }
        public IEnumerable<string> Engines { get; set; }
        public LanguageDetectionDto LanguageDetection { get; set; }
        public ContextResultsDto ContextResults { get; set; }
        public int TimeTaken { get; set; }
        public bool Truncated { get; set; }
    }
}
