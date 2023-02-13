using ReversoAPI.Web.Models.Responses;
using ReversoAPI.Web.Models.Values;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Clients.Interfaces
{
    public interface ISynonimsClient
    {
        Task<SynonymsResponse> GetAsync(string text, Language language);
    }
}
