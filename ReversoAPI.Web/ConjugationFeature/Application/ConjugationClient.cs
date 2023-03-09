using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.ConjugationFeature.Domain.Core.Interfaces.Entities;
using ReversoAPI.Web.ConjugationFeature.Application.Validators;
using ReversoAPI.Web.ConjugationFeature.Application.Interfaces.Services;
using ReversoAPI.Web.ConjugationFeature.Application.Interfaces;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.ConjugationFeature.Application
{
    public class ConjugationClient : IConjugationClient
    {
        private readonly IConjugationService _conjugationService;

        public ConjugationClient(IConjugationService conjugationService) => _conjugationService = conjugationService;

        public async Task<IConjugationData> GetAsync(string text, Language language, CancellationToken cancellationToken = default)
        {
            var validationResult = new ConjugationRequestValidator(text, language).Validate();
            if (!validationResult.IsValid) throw validationResult.Exception;

            var output = await _conjugationService.GetAsync(text, language, cancellationToken).ConfigureAwait(false);
            return output;
        }
    }
}
