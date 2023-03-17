using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.TranslationFeature.Domain.Entities;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.TranslationFeature.Application.Interfaces
{
    public interface ITranslationClient
    {
        Task<TranslationData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default);
    }
}