using ReversoAPI.Web.Conjugation.App.Interfaces;
using ReversoAPI.Web.Context.App.Interfaces;
using ReversoAPI.Web.GrammarCheck.App.Interfaces;
using ReversoAPI.Web.Pronunciation.App.Interfaces;
using ReversoAPI.Web.Synonyms.App.Interfaces;
using ReversoAPI.Web.Translation.App.Interfaces;

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