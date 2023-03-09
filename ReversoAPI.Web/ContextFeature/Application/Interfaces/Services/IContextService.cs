using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.ContextFeature.Domain.Core.Interfaces.Entities;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.ContextFeature.Application.Interfaces.Services
{
    public interface IContextService
    {
        Task<IContextData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default);
    }
}