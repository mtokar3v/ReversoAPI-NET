using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.Values;
using System.Collections.Generic;

namespace ReversoAPI.Web.Conjugation.Domain.Core.Entities
{
    public class ConjugationData : IConjugationData
    {
        public string Text { get; set; }
        public Language Language { get; set; }
        public IDictionary<string, IEnumerable<Conjugation>> Conjugations { get; set; }
    }
}
