using ReversoAPI.Web;
using ReversoAPI.Web.ConjugationFeature.Application.Services;
using ReversoAPI.Web.ContextFeature.Application.Services;
using ReversoAPI.Web.GrammarCheckFeature.Application.Services;
using ReversoAPI.Web.PronunciationFeature.Application.Services;
using ReversoAPI.Web.SynonymsFeature.Application.Services;
using ReversoAPI.Web.TranslationFeature.Application.Services;
using System;

namespace ReversoAPI
{
    public class ReversoClient : IReversoClient
    {
        private IHttpClient _httpClient;

        public ReversoClient(ReversoClientConfig config)
        {
            _httpClient = config.HttpClient;
            var apiConnector = config.APIConnector;

            Context = new ContextClient(new ContextService(apiConnector, config.ContextParser));
            Synonyms = new SynonymsClient(new SynonymsService(apiConnector, config.SynonymsParser));
            Conjugation = new ConjugationClient(new ConjugationService(apiConnector, config.ConjugationParser));

            Spelling = new SpellingClient(new SpellingService(apiConnector, config.Logger));
            Translation = new TranslationClient(new TranslationService(apiConnector, config.Logger));
            Pronunciation = new PronunciationClient(new PronunciationService(apiConnector));
        }

        public ReversoClient() : this(new ReversoClientConfig().CreateDefault())
        {
        }

        public IContextClient Context { get; }
        public ISynonymsClient Synonyms { get; }
        public ISpellingClient Spelling { get; }
        public ITranslationClient Translation { get; }
        public IPronunciationClient Pronunciation { get; }
        public IConjugationClient Conjugation { get; }

        public void Dispose()
        {
            _httpClient?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
