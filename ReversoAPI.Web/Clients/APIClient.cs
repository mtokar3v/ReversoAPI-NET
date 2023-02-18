using ReversoAPI.Web.Http.Interfaces;

namespace ReversoAPI.Web.Clients
{
    public abstract class APIClient
    {
        protected readonly IAPIConnector _apiConnector;
        public APIClient(IAPIConnector apiConnector)
        {
            _apiConnector = apiConnector;
        }
    }
}
