using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Values;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Clients.Interfaces
{
    public interface IContextClient
    {
        Task<ContextData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default);
    }
}