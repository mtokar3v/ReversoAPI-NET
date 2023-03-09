using ReversoAPI.Web.ConjugationFeature.Application.Interfaces;
using ReversoAPI.Web.ContextFeature.Application.Interfaces;
using ReversoAPI.Web.GrammarCheckFeature.Application.Interfaces;
using ReversoAPI.Web.PronunciationFeature.Application.Interfaces;
using ReversoAPI.Web.SynonymsFeature.Application.Interfaces;
using ReversoAPI.Web.TranslationFeature.Application.Interfaces;

namespace ReversoAPI.Web
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