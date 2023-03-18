using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.GrammarCheckFeature.Application.Interfaces.Services
{
    public interface ISpellingService
    {
        Task<SpellingData> GetAsync(string text, Language language, Locale locale = Locale.None, CancellationToken cancellationToken = default);
    }
}