using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.GrammarCheckFeature.Domain.Entities;
using ReversoAPI.Web.GrammarCheckFeature.Domain.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.GrammarCheckFeature.Application.Interfaces
{
    public interface ISpellingClient
    {
        Task<SpellingData> GetAsync(string text, Language language, Locale locale = Locale.None, CancellationToken cancellationToken = default);
    }
}