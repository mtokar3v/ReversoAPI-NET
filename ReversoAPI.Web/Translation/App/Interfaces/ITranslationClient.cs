using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.Translation.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Translation.App.Interfaces
{
    public interface ITranslationClient
    {
        Task<TranslationData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default);
    }
}