using System.Collections.Generic;
using ReversoAPI.Web.ConjugationFeature.Domain.Core.Interfaces.Entities;
using ReversoAPI.Web.ConjugationFeature.Domain.Core.Interfaces.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.ConjugationFeature.Domain.Core.Entities
{
    public class ConjugationData : IConjugationData
    {
        public string Text { get; set; }
        public Language Language { get; set; }
        public IDictionary<string, IEnumerable<IConjugation>> Conjugations { get; set; }
    }
}
