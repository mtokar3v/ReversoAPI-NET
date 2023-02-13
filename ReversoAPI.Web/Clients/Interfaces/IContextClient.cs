using ReversoAPI.Web.Models.Responses;
using ReversoAPI.Web.Models.Values;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Clients.Interfaces
{
    public interface IContextClient
    {
        Task<ContextResponse> GetAsync(string text, Language source, Language target);
    }
}