using ReversoAPI.Web.Clients.Interfaces;

namespace ReversoAPI.Web.Clients
{
    public class ReversoClient : IReversoClient
    {
        public ReversoClient()
        {
            Context = new ContextClient();
            Symonims = new SynonimsClient();
        }

        public IContextClient Context { get; }
        public ISynonimsClient Symonims { get; }
    }
}
