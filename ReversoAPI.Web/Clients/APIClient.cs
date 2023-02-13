using ReversoAPI.Web.Http;
using ReversoAPI.Web.Http.Interfaces;

namespace ReversoAPI.Web.Clients
{
    public abstract class APIClient
    {
        protected readonly IAPIConnector API;
        public APIClient() => API = new APIConnector();
    }
}
