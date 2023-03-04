using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Values;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Clients.Interfaces
{
    public interface ISpellingClient
    {
        Task<SpellingData> GetAsync(string text, Language language, Locale locale = Locale.None, CancellationToken cancellationToken = default);
    }
}