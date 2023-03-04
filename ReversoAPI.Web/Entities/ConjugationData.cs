using ReversoAPI.Web.Values;
using System.Collections.Generic;

namespace ReversoAPI.Web.Entities
{
    public class ConjugationData
    {
        public string Text { get; set; }
        public Language Language { get; set; }
        public IDictionary<string, IEnumerable<Conjugation>> Conjugations { get; set; }
    }
}
