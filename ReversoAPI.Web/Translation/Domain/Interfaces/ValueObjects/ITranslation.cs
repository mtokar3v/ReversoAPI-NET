using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.Translation.Domain.Interfaces.ValueObjects
{
    public interface ITranslation
    {
        int? Frequency { get; }
        bool? IsColloquial { get; }
        bool? IsRude { get; }
        PartOfSpeech? PartOfSpeech { get; }
        Language Source { get; }
        Language Target { get; }
        string Text { get; }
        string Transliteration { get; }
        string Value { get; }
    }
}