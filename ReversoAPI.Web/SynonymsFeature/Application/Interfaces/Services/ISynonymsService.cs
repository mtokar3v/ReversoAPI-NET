using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.SynonymsFeature.Domain.Core.Entities;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.SynonymsFeature.Application.Interfaces.Services
{
    public interface ISynonymsService
    {
        Task<SynonymsData> GetAsync(string text, Language language, CancellationToken cancellationToken = default);
    }
}