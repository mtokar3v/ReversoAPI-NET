using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI
{
    public interface ISpellingClient
    {
        Task<SpellingData> GetAsync(string text, Language language, Locale locale = Locale.None, CancellationToken cancellationToken = default);
    }
}