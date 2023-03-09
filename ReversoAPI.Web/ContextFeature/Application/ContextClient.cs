using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.ContextFeature.Application.Interfaces;
using ReversoAPI.Web.ContextFeature.Application.Interfaces.Services;
using ReversoAPI.Web.ContextFeature.Application.Validators;
using ReversoAPI.Web.ContextFeature.Domain.Core.Interfaces.Entities;

namespace ReversoAPI.Web.ContextFeature.Application
{
    public class ContextClient : IContextClient
    {
        private readonly IContextService _contextService;

        public ContextClient(IContextService contextService) => _contextService = contextService;

        public async Task<IContextData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default)
        {
            var validationResult = new ContextRequestValidator(text, source, target).Validate();
            if (!validationResult.IsValid) throw validationResult.Exception;

            var output = await _contextService.GetAsync(text, source, target, cancellationToken).ConfigureAwait(false);
            return output;
        }
    }
}
