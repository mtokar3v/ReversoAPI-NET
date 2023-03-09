using System;
using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.GrammarCheckFeature.Application.DTOs;
using ReversoAPI.Web.GrammarCheckFeature.Application.Interfaces.Services;
using ReversoAPI.Web.GrammarCheckFeature.Domain.ValueObjects;
using ReversoAPI.Web.GrammarCheckFeature.Application.Validators;
using ReversoAPI.Web.GrammarCheckFeature.Domain.Interfaces.Entities;
using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.Shared.Application.Extensions;
using ReversoAPI.Web.Shared.Infrastructure.Http.Interfaces;

namespace ReversoAPI.Web.GrammarCheckFeature.Application.Services
{
    public class SpellingService : ISpellingService
    {
        private const string SpellingURL = "https://orthographe.reverso.net/api/v1/Spelling/";

        private readonly IAPIConnector _apiConnector;
        public SpellingService(IAPIConnector apiConnector) => _apiConnector = apiConnector;

        public async Task<ISpellingData> GetAsync(string text, Language language, Locale locale = Locale.None, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));

            using var response = await _apiConnector
                .PostAsync(new Uri(SpellingURL), new SpellingRequest(text, language, locale), cancellationToken)
                .ConfigureAwait(false);

            var spellingDto = response.Content.Deserialize<SpellingResponse>();

            var validationResult = new SpellingResponseValidator(spellingDto).Validate();
            if (!validationResult.IsValid) return null;

            return spellingDto.ToModel();
        }
    }
}
