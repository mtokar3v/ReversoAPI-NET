using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Values;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Clients.Interfaces
{
    public interface ISynonymsClient
    {
        Task<SynonymsData> GetAsync(string text, Language language, CancellationToken cancellationToken = default);
    }
}
