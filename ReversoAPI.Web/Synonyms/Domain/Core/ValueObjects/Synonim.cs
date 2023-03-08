using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.Values;

namespace ReversoAPI.Web.Synonyms.Domain.Core.ValueObjects
{
    public class Synonim : ISynonim
    {
        public Synonim(string value, Language language, PartOfSpeech partOfSpeech)
        {
            Value = value;
            Language = language;
            PartOfSpeech = partOfSpeech;
        }

        public string Value { get; }
        public Language Language { get; }
        public PartOfSpeech PartOfSpeech { get; }
    }
}
