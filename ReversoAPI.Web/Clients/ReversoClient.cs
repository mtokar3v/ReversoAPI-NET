using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.Tools.Parsers;

namespace ReversoAPI.Web.Clients
{
    public class ReversoClient : IReversoClient
    {
        public ReversoClient()
        {
            Context = new ContextClient(new ContextResponseParser());
            Symonims = new SynonymsClient(new SynonymsResponseParser());
        }

        public IContextClient Context { get; }
        public ISynonimsClient Symonims { get; }
    }
}
