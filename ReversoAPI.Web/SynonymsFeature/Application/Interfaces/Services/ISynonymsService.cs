using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.SynonymsFeature.Application.Interfaces.Services
{
    public interface ISynonymsService
    {
        Task<SynonymsData> GetAsync(string text, Language language, CancellationToken cancellationToken = default);
    }
}