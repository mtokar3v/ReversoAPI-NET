namespace ReversoAPI.Web.Values
{
    public class Word
    {
        public Word(string value, Language language, PartOfSpeech partOfSpeech)
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
