using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.ContextFeature.Application.Interfaces.Services
{
    public interface IContextService
    {
        Task<ContextData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default);
    }
}