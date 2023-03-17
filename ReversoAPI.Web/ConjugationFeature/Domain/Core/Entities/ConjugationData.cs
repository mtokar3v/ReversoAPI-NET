using System.Collections.Generic;
using ReversoAPI.Web.ConjugationFeature.Domain.Core.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.ConjugationFeature.Domain.Core.Entities
{
    public class ConjugationData
    {
        public string Text { get; set; }
        public Language Language { get; set; }
        public IDictionary<string, IEnumerable<Conjugation>> Conjugations { get; set; }
    }
}
