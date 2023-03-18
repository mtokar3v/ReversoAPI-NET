namespace ReversoAPI
{
    public class Synonim
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
