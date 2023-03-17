using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.ConjugationFeature.Application.Interfaces.Services
{
    public interface IConjugationService
    {
        Task<ConjugationData> GetAsync(string text, Language language, CancellationToken cancellationToken = default);
    }
}