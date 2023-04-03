using System;
using ReversoAPI.Web.Shared.Infrastructure.Http;
using ReversoAPI.Web;

namespace ReversoAPI
{
    public class ReversoClientConfig
    {
        // General
        private IHttpClient _httpClient;
        public IHttpClient HttpClient 
        { 
            get => _httpClient ?? new SimpleHttpClient();
            private set => _httpClient = value;
        }

        private IAPIConnector _apiConnector;
        public IAPIConnector APIConnector 
        {
            get => _apiConnector ?? new APIConnector(new SimpleHttpClient());
            private set => _apiConnector = value;
        }

        private IParseService<ContextData> _contextParser;
        public IParseService<ContextData> ContextParser 
        {
            get => _contextParser ?? new ContextParseService(Logger);
            private set => _contextParser = value;
        }

        private IParseService<SynonymsData> _synonymsParser;
        public IParseService<SynonymsData> SynonymsParser 
        {
            get => _synonymsParser ?? new SynonymsParseService(Logger);
            private set => _synonymsParser = value;
        }

        private IParseService<ConjugationData> _conjugationParser;
        public IParseService<ConjugationData> ConjugationParser
        {
            get => _conjugationParser ?? new ConjugationParseService(Logger);
            private set => _conjugationParser = value;
        }

        // Extra
        public ILogger Logger { get; private set; }

        public ReversoClientConfig() { }

        public ReversoClientConfig(
            IHttpClient httpClient,
            IAPIConnector apiConnector,
            IParseService<ContextData> contextParser, 
            IParseService<SynonymsData> synonymsParser,
            IParseService<ConjugationData> conjugationParser,
            ILogger logger)
        {
            HttpClient = httpClient;
            APIConnector = apiConnector;
            ContextParser = contextParser;
            SynonymsParser = synonymsParser;
            ConjugationParser = conjugationParser;
            Logger = logger;
        }

        public ReversoClientConfig CreateDefault()
        {
            var httpClient = new SimpleHttpClient();

            return new ReversoClientConfig(
                httpClient,
                new APIConnector(httpClient),
                new ContextParseService(null),
                new SynonymsParseService(null),
                new ConjugationParseService(null),
                null);
        }

        public ReversoClientConfig WithHttpClient(IHttpClient httpClient)
        {
            if(httpClient is null) throw new ArgumentNullException(nameof(httpClient));

            HttpClient = httpClient;
            return this;
        }

        public ReversoClientConfig WithApiConnector(IAPIConnector apiConnector)
        {
            if (apiConnector is null) throw new ArgumentNullException(nameof(apiConnector));

            APIConnector = apiConnector;
            return this;
        }

        public ReversoClientConfig WithContextParseService(IParseService<ContextData> contextParser)
        {
            if (contextParser is null) throw new ArgumentNullException(nameof(contextParser));

            ContextParser = contextParser;
            return this;
        }

        public ReversoClientConfig WithSynonymsParseService(IParseService<SynonymsData> synonymsParser)
        {
            if (synonymsParser is null) throw new ArgumentNullException(nameof(synonymsParser));

            SynonymsParser = synonymsParser;
            return this;
        }

        public ReversoClientConfig WithConjugationParseService(IParseService<ConjugationData> conjugationParser)
        {
            if (conjugationParser is null) throw new ArgumentNullException(nameof(conjugationParser));

            ConjugationParser = conjugationParser;
            return this;
        }

        public ReversoClientConfig WithLogger(ILogger logger)
        {
            Logger = logger;
            return this;
        }
    }
}
