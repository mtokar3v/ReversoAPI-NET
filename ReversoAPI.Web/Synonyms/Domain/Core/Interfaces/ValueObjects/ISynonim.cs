using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.Values;

namespace ReversoAPI.Web.Synonyms.Domain.Core.Interfaces.ValueObjects
{
    public interface ISynonim
    {
        Language Language { get; }
        PartOfSpeech PartOfSpeech { get; }
        string Value { get; }
    }
}