using System.Collections.Generic;

namespace ReversoAPI
{
    public class TranslationData
    {
        public string Text { get; set; }
        public Language Source { get; set; }
        public Language Target { get; set; }
        public IEnumerable<Translation> Translations { get; set; }
    }
}
