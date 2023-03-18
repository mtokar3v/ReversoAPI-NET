using System.Collections.Generic;

namespace ReversoAPI
{ 
    public class SpellingData
    {
        public string Text { get; set; }
        public Language Language { get; set; }
        public IEnumerable<Correction> Correction { get; set; }
    }
}
