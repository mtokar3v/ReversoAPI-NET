using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.Conjugation.Domain.Core.Interfaces.Entities
{
    public interface IConjugationData
    {
        System.Collections.Generic.IDictionary<string, System.Collections.Generic.IEnumerable<Conjugation>> Conjugations { get; set; }
        Language Language { get; set; }
        string Text { get; set; }
    }
}