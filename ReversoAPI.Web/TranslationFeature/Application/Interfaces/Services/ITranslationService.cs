using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.TranslationFeature.Domain.Interfaces.Entities;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.TranslationFeature.Application.Interfaces.Services
{
    public interface ITranslationService
    {
        Task<ITranslationData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default);
    }
}