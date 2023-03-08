using ReversoAPI.Web.Conjugation.App.Interfaces;
using ReversoAPI.Web.Context.App.Interfaces;
using ReversoAPI.Web.GrammarCheck.App;
using ReversoAPI.Web.GrammarCheck.App.Interfaces;
using ReversoAPI.Web.Pronunciation.App.Interfaces;
using ReversoAPI.Web.Shared.Infrastructure.Http;
using ReversoAPI.Web.Synonyms.App.Interfaces;
using ReversoAPI.Web.Tools.Parsers;
using ReversoAPI.Web.Translation.App.Interfaces;

namespace ReversoAPI.Web
{
    public class ReversoClient : IReversoClient
    {
        public ReversoClient()
        {
            var apiConnector = APIConnector.Create(HttpClientCacheWrapper.GetInstance());

            Context = new ContextClient(apiConnector, new ContextResponseParser());
            Synonyms = new SynonymsClient(apiConnector, new SynonymsResponseParser());
            Conjugation = new ConjugationClient(apiConnector, new ConjugationResponseParser());

            Spelling = new SpellingClient(apiConnector);
            Translation = new TranslationClient(apiConnector);
            Pronunciation = new PronunciationClient(apiConnector);
        }

        public IContextClient Context { get; }
        public ISynonymsClient Synonyms { get; }
        public ISpellingClient Spelling { get; }
        public ITranslationClient Translation { get; }
        public IPronunciationClient Pronunciation { get; }
        public IConjugationClient Conjugation { get; }
    }
}
