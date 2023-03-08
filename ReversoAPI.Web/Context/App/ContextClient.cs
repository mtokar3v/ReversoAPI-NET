using System;
using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.Values.Validators;
using ReversoAPI.Web.Domain.Core.Context.Services;
using ReversoAPI.Web.Domain.Core.Context.Enities;
using ReversoAPI.Web.Domain.Generic.ValueObjects;
using ReversoAPI.Web.Context.App.Interfaces.Clients;

namespace ReversoAPI.Web.Context.App
{
    public class ContextClient : IContextClient
    {
        private readonly IContextService _contextService;

        public ContextClient(IContextService contextService) => _contextService = contextService;

        public async Task<ContextData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default)
        {
            var validationResult = new ContextRequestValidator(text, source, target).Validate();
            if (!validationResult.IsValid) throw validationResult.Exception;

            var output = await _contextService.GetAsync(text, source, target, cancellationToken);
            return output;
        }
    }
}
