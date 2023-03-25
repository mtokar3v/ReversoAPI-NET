using ReversoAPI.Web.ContextFeature.Domain.Core.Services;
using ReversoAPI.Web.Shared.Domain.Interfaces.Services;
using ReversoAPI.Web.Shared.Infrastructure.Http;
using ReversoAPI.Web.Shared.Infrastructure.Http.Interfaces;
using ReversoAPI.Web.Shared.Infrastructure.Logger;
using ReversoAPI.Web.SynonymsFeature.Domain.Core.Services;
using System;

namespace ReversoAPI.Web
{
    public class ReversoClientConfig
    {
        // General
        public IHttpClient HttpClient { get; private set; }
        public IAPIConnector APIConnector { get; private set; }

        // Let's fill up only elementary fields
        // complex objects neither will be filled by user or will be setted by default
        private IParser<ContextData> _contextParser;
        public IParser<ContextData> ContextParser 
        {
            get => _contextParser ?? new ContextParserService(Logger);
            private set => _contextParser = value;
        }
        public IParser<SynonymsData> SynonymsParser { get; private set; }

        // Extra
        public ILogger Logger { get; private set; }

        private ReversoClientConfig(
            IHttpClient httpClient,
            IAPIConnector apiConnector,
            IParser<ContextData> contextParser, 
            IParser<SynonymsData> synonymsParser,
            ILogger logger)
        {
            HttpClient = httpClient;
            APIConnector = apiConnector;
            ContextParser = contextParser;
            Logger = logger;
        }

        ReversoClientConfig CreateDefault()
        {
            var httpClient = new CachedHttpClient();

            return new ReversoClientConfig(
                httpClient,
                new APIConnector(httpClient),
                new ContextParserService(null),
                new SynonymsParserService(null),
                null);
        }

        ReversoClientConfig WithHttpClient(IHttpClient httpClient)
        {
            if(httpClient is null) throw new ArgumentNullException(nameof(httpClient));

            HttpClient = httpClient;
            return this;
        }

        ReversoClientConfig WithApiConnector(IAPIConnector apiConnector)
        {
            if (apiConnector is null) throw new ArgumentNullException(nameof(apiConnector));

            APIConnector = apiConnector;
            return this;
        }

        ReversoClientConfig WithLogger(ILogger logger)
        {
            Logger = logger;
            return this;
        }
    }
}
