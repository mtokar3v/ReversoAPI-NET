using ReversoAPI.Web.Domain.Core.Context.Enities;
using ReversoAPI.Web.Domain.Generic.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Context.App.Interfaces.Services
{
    internal interface IContextService
    {
        Task<ContextData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default);
    }
}