using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Values;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Clients.Interfaces
{
    public interface IConjugationClient
    {
        Task<ConjugationData> GetAsync(string text, Language language);
    }
}