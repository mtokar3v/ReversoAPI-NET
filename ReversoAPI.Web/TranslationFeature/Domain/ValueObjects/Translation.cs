namespace ReversoAPI
{
    public class Translation
    {
        public Translation(
            string originalText,
            string translatedText,
            Language source,
            Language target,
            string transliteration = null,
            PartOfSpeech? partOfSpeech = null,
            int? frequency = null,
            bool? isColloquial = null,
            bool? isRude = null)
        {
            Text = originalText;
            Value = translatedText;
            Source = source;
            Target = target;
            Transliteration = transliteration;
            PartOfSpeech = partOfSpeech;
            Frequency = frequency;
            IsColloquial = isColloquial;
            IsRude = isRude;
        }

        public string Text { get; }
        public string Value { get; }

        public Language Source { get; }
        public Language Target { get; }


        #region Optional Fields

        // Some words and sentences does not have these properties
        public string Transliteration { get; }
        public PartOfSpeech? PartOfSpeech { get; }
        public int? Frequency { get; }
        public bool? IsColloquial { get; }
        public bool? IsRude { get; }

        #endregion
    }
}
