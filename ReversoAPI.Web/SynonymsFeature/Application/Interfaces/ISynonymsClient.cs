using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI
{
    public interface ISynonymsClient
    {
        Task<SynonymsData> GetAsync(string text, Language language, CancellationToken cancellationToken = default);
    }
}
