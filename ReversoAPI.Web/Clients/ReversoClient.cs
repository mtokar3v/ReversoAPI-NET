using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.Http;
using ReversoAPI.Web.Tools.Parsers;

namespace ReversoAPI.Web.Clients
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
