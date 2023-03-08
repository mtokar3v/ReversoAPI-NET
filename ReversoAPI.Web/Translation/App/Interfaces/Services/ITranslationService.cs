using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.Translation.App.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Translation.App.Interfaces.Services
{
    internal interface ITranslationService
    {
        Task<TranslationService> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default);
    }
}