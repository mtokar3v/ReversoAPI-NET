using ReversoAPI.Web.Models.Responses;
using ReversoAPI.Web.Models.Values;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Clients.Interfaces
{
    public interface ISynonymsClient
    {
        Task<SynonymsResponse> GetAsync(string text, Language language);
    }
}
