using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.ConjugationFeature.Domain.Core.Interfaces.Entities;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.ConjugationFeature.Application.Interfaces
{
    public interface IConjugationClient
    {
        Task<IConjugationData> GetAsync(string text, Language language, CancellationToken cancellationToken = default);
    }
}