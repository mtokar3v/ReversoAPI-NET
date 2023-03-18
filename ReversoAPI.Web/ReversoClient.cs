using ReversoAPI.Web.ConjugationFeature.Application.Services;
using ReversoAPI.Web.ConjugationFeature.Domain.Core.Services;
using ReversoAPI.Web.ContextFeature.Application.Services;
using ReversoAPI.Web.ContextFeature.Domain.Core.Services;
using ReversoAPI.Web.GrammarCheckFeature.Application.Services;
using ReversoAPI.Web.PronunciationFeature.Application.Services;
using ReversoAPI.Web.SynonymsFeature.Application.Services;
using ReversoAPI.Web.SynonymsFeature.Domain.Core.Services;
using ReversoAPI.Web.TranslationFeature.Application.Services;
using ReversoAPI.Web.Shared.Infrastructure.Http;

namespace ReversoAPI
{
    public class ReversoClient : IReversoClient
    {
        public ReversoClient()
        {
            var apiConnector = APIConnector.Create(HttpClientCacheWrapper.GetInstance());

            Context = new ContextClient(new ContextService(apiConnector, new ContextParserService()));
            Synonyms = new SynonymsClient(new SynonymsService(apiConnector, new SynonymsParserService()));
            Conjugation = new ConjugationClient(new ConjugationService(apiConnector, new ConjugationParserService()));

            Spelling = new SpellingClient(new SpellingService(apiConnector));
            Translation = new TranslationClient(new TranslationService(apiConnector));
            Pronunciation = new PronunciationClient(new PronunciationService(apiConnector));
        }

        public IContextClient Context { get; }
        public ISynonymsClient Synonyms { get; }
        public ISpellingClient Spelling { get; }
        public ITranslationClient Translation { get; }
        public IPronunciationClient Pronunciation { get; }
        public IConjugationClient Conjugation { get; }
    }
}
