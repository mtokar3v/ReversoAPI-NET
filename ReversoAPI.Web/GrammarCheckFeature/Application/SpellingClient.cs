using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.GrammarCheckFeature.Application.Interfaces.Services;
using ReversoAPI.Web.GrammarCheckFeature.Application.Validators;

namespace ReversoAPI
{
    public class SpellingClient : ISpellingClient
    {
        private readonly ISpellingService _spellingService;

        public SpellingClient(ISpellingService spellingService) => _spellingService = spellingService;

        public async Task<SpellingData> GetAsync(string text, Language language, Locale locale = Locale.None, CancellationToken cancellationToken = default)
        {
            var validationResult = new SpellingRequestValidator(text, language, locale).Validate();
            if (!validationResult.IsValid) throw validationResult.Exception;

            var output = await _spellingService
                .GetAsync(text, language, locale, cancellationToken)
                .ConfigureAwait(false);

            return output;
        }
    }
}
