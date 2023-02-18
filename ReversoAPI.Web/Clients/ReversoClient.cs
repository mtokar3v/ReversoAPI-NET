﻿using ReversoAPI.Web.Clients.Interfaces;
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
            Symonims = new SynonymsClient(apiConnector, new SynonymsResponseParser());
        }

        public IContextClient Context { get; }
        public ISynonimsClient Symonims { get; }
    }
}
