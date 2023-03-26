using System;
using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.GrammarCheckFeature.Application.DTOs;
using ReversoAPI.Web.GrammarCheckFeature.Application.Interfaces.Services;
using ReversoAPI.Web.GrammarCheckFeature.Application.Validators;
using ReversoAPI.Web.Shared.Application.Extensions;
using ReversoAPI.Web.Shared.Infrastructure.Http.Interfaces;
using ReversoAPI.Web.Shared.Infrastructure.Logger;

namespace ReversoAPI.Web.GrammarCheckFeature.Application.Services
{
    public class SpellingService : ISpellingService
    {
        private const string SpellingURL = "https://orthographe.reverso.net/api/v1/Spelling/";

        private readonly ILogger _log;
        private readonly IAPIConnector _apiConnector;

        public SpellingService(IAPIConnector apiConnector, ILogger log)
        {
            _log = log;
            _apiConnector = apiConnector;
        }

        public async Task<SpellingData> GetAsync(string text, Language language, Locale locale = Locale.None, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));

            using var response = await _apiConnector
                .PostAsync(new Uri(SpellingURL), new SpellingRequest(text, language, locale), cancellationToken)
                .ConfigureAwait(false);

            var spellingDto = response.Content.Deserialize<SpellingResponse>();

            var validationResult = new SpellingResponseValidator(spellingDto).Validate();
            if (!validationResult.IsValid) 
            {
                _log?.Error(validationResult.Message);
                return null;
            }

            return spellingDto.ToModel();
        }
    }
}
