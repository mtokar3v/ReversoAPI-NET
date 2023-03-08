using ReversoAPI.Web.Context.Domain.Core.Enities;
using ReversoAPI.Web.Shared.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Context.App.Interfaces
{
    public interface IContextClient
    {
        Task<ContextData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default);
    }
}