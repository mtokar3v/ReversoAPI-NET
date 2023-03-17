using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.SynonymsFeature.Domain.Core.Entities;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.SynonymsFeature.Application.Interfaces
{
    public interface ISynonymsClient
    {
        Task<SynonymsData> GetAsync(string text, Language language, CancellationToken cancellationToken = default);
    }
}
