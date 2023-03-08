using ReversoAPI.Web.Conjugation.Domain.Core.Entities;
using ReversoAPI.Web.Shared.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Conjugation.App.Interfaces
{
    public interface IConjugationClient
    {
        Task<ConjugationData> GetAsync(string text, Language language, CancellationToken cancellationToken = default);
    }
}