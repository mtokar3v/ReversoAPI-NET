using System.Collections.Generic;

namespace ReversoAPI
{
    public class ConjugationData
    {
        public string Text { get; set; }
        public Language Language { get; set; }
        public IDictionary<string, IEnumerable<Conjugation>> Conjugations { get; set; }
    }
}
