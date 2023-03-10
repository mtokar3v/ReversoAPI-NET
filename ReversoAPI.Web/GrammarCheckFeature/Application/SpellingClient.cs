using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.GrammarCheckFeature.Domain.ValueObjects;
using ReversoAPI.Web.GrammarCheckFeature.Domain.Interfaces.Entities;
using ReversoAPI.Web.GrammarCheckFeature.Application.Interfaces.Services;
using ReversoAPI.Web.GrammarCheckFeature.Application.Interfaces;
using ReversoAPI.Web.GrammarCheckFeature.Application.Validators;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.GrammarCheckFeature.Application
{
    public class SpellingClient : ISpellingClient
    {
        private readonly ISpellingService _spellingService;

        public SpellingClient(ISpellingService spellingService) => _spellingService = spellingService;

        public async Task<ISpellingData> GetAsync(string text, Language language, Locale locale = Locale.None, CancellationToken cancellationToken = default)
        {
            var validationResult = new SpellingRequestValidator(text, language, locale).Validate();
            if (!validationResult.IsValid) throw validationResult.Exception;

            var output = await _spellingService.GetAsync(text, language, locale, cancellationToken).ConfigureAwait(false);
            return output;
        }
    }
}
