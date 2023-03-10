using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.SynonymsFeature.Domain.Core.Interfaces.Entities;

namespace ReversoAPI.Web.SynonymsFeature.Application.Interfaces.Services
{
    public interface ISynonymsService
    {
        Task<ISynonymsData> GetAsync(string text, Language language, CancellationToken cancellationToken = default);
    }
}