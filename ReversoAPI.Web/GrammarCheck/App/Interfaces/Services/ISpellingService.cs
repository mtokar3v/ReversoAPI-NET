using ReversoAPI.Web.GrammarCheck.Domain.Entities;
using ReversoAPI.Web.GrammarCheck.Domain.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.GrammarCheck.App.Interfaces.Services
{
    internal interface ISpellingService
    {
        Task<SpellingData> GetAsync(string text, Language language, Locale locale = Locale.None, CancellationToken cancellationToken = default);
    }
}