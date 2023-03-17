using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.ContextFeature.Domain.Core.Enities;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.ContextFeature.Application.Interfaces
{
    public interface IContextClient
    {
        Task<ContextData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default);
    }
}