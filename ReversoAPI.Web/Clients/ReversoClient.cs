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

            Spelling = new SpellingClient(apiConnector);
            Translation = new TranslationClient(apiConnector);
        }

        public IContextClient Context { get; }
        public ISynonymsClient Synonyms { get; }
        public ISpellingClient Spelling { get; }
        public ITranslationClient Translation { get; }
    }
}
