using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI
{
    public interface IConjugationClient
    {
        Task<ConjugationData> GetAsync(string text, Language language, CancellationToken cancellationToken = default);
    }
}