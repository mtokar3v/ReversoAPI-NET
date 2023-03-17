using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI
{
    public interface IContextClient
    {
        Task<ContextData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default);
    }
}