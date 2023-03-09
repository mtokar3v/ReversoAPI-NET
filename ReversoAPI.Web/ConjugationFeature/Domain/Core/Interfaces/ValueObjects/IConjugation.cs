using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.ConjugationFeature.Domain.Core.Interfaces.ValueObjects
{
    public interface IConjugation
    {
        string Group { get; }
        Language Language { get; }
        string Verb { get; }
    }
}