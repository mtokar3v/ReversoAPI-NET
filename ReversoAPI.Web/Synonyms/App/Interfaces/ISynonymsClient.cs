using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.Synonyms.Domain.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Synonyms.App.Interfaces
{
    public interface ISynonymsClient
    {
        Task<SynonymsData> GetAsync(string text, Language language, CancellationToken cancellationToken = default);
    }
}
