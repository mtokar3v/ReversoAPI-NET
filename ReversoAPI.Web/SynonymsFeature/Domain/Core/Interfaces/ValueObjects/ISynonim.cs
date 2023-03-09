using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.SynonymsFeature.Domain.Core.Interfaces.ValueObjects
{
    public interface ISynonim
    {
        Language Language { get; }
        PartOfSpeech PartOfSpeech { get; }
        string Value { get; }
    }
}