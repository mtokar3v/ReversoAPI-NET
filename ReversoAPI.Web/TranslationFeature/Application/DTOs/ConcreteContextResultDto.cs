using System.Collections.Generic;

namespace ReversoAPI.Web.TranslationFeature.Application.DTOs
{
    public class ConcreteContextResultDto
    {
        public string Translation { get; set; }
        public string Transliteration { get; set; }
        public bool Colloquial { get; set; }
        public int Frequency { get; set; }
        public bool Rude { get; set; }
        public string PartOfSpeech { get; set; }
        public object Vowels { get; set; } // do research
        public IEnumerable<string> SourceExamples { get; set; }
        public IEnumerable<string> TargetExamples { get; set; }
    }
}
