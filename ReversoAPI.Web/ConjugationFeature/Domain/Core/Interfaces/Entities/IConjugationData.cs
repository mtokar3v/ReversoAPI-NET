using System.Collections.Generic;
using ReversoAPI.Web.ConjugationFeature.Domain.Core.Interfaces.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.ConjugationFeature.Domain.Core.Interfaces.Entities
{
    public interface IConjugationData
    {
        IDictionary<string, IEnumerable<IConjugation>> Conjugations { get; set; }
        Language Language { get; set; }
        string Text { get; set; }
    }
}