using ReversoAPI.Web.Clients.Interfaces;

namespace ReversoAPI.Web.Clients
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