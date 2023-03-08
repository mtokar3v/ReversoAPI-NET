using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Shared.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Conjugation.App.Interfaces.Services
{
    internal interface IConjugationService
    {
        Task<ConjugationData> GetAsync(string text, Language language, CancellationToken cancellationToken = default);
    }
}