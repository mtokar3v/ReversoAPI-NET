using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.TranslationFeature.Application.Interfaces.Services
{
    public interface ITranslationService
    {
        Task<TranslationData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default);
    }
}