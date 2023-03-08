using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.Translation.App.DTOs;
using ReversoAPI.Web.Translation.App.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ReversoAPI.Web.Translation.App.Services
{
    internal class TranslationService : ITranslationService
    {
        private const string TranslationURL = "https://api.reverso.net/translate/v1/translation";

        private readonly IAPIConnector _apiConnector;
        public TranslationService(IAPIConnector apiConnector) => _apiConnector = apiConnector;

        public async Task<TranslationService> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));

            using var response = await _apiConnector
                .PostAsync(new Uri(TranslationURL), new TranslationRequest(text, source, target), cancellationToken)
                .ConfigureAwait(false);

            var translationDto = response.Content.Deserialize<TranslationResponse>();

            validationResult = new TranslationResponseValidator(translationDto).Validate();
            if (!validationResult.IsValid) return null;

            return translationDto.ToModel();
        }
    }
}
