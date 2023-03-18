namespace ReversoAPI
{
    public interface IReversoClient
    {
        IContextClient Context { get; }
        ISynonymsClient Synonyms { get; }
        ISpellingClient Spelling { get; }
        ITranslationClient Translation { get; }
        IPronunciationClient Pronunciation { get; }
        IConjugationClient Conjugation { get; }
    }
}