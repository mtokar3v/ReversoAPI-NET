using ReversoAPI.Web.DTOs.SpellingResponseData;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.GrammarCheck.App.Validators;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.Values;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ReversoAPI.Web.GrammarCheck.App.Services
{
    internal class SpellingService
    {
        private const string SpellingURL = "https://orthographe.reverso.net/api/v1/Spelling/";

        private readonly IAPIConnector _apiConnector;
        public SpellingService(IAPIConnector apiConnector) => _apiConnector = apiConnector;

        public async Task<SpellingData> GetAsync(string text, Language language, Locale locale = Locale.None, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));

            using var response = await _apiConnector
                .PostAsync(new Uri(SpellingURL), new SpellingRequest(text, language, locale), cancellationToken)
                .ConfigureAwait(false);

            var spellingDto = response.Content.Deserialize<SpellingResponse>();

            validationResult = new SpellingResponseValidator(spellingDto).Validate();
            if (!validationResult.IsValid) return null;

            return spellingDto.ToModel();
        }
    }
}
