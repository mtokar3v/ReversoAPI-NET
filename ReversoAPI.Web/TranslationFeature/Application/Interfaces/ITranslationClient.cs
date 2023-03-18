using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI
{
    public interface ITranslationClient
    {
        Task<TranslationData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default);
    }
}