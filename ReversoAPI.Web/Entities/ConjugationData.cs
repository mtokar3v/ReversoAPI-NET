using ReversoAPI.Web.Values;
using ReversoAPI.Web.Values.ConjugationObjects;
using System.Collections.Generic;

namespace ReversoAPI.Web.Entities
{
    public class ConjugationData
    {
        public Language Language { get; set; }
        public IDictionary<string, IEnumerable<Conjugation>> Conjugations { get; set; }
    }
}
