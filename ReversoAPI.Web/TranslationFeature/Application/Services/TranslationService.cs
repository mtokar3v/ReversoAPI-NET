﻿using System;
using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.TranslationFeature.Application.DTOs;
using ReversoAPI.Web.TranslationFeature.Application.Interfaces.Services;
using ReversoAPI.Web.TranslationFeature.Application.Validators;
using ReversoAPI.Web.Shared.Application.Extensions;

namespace ReversoAPI.Web.TranslationFeature.Application.Services
{
    public class TranslationService : ITranslationService
    {
        private const string TranslationURL = "https://api.reverso.net/translate/v1/translation";

        private readonly IAPIConnector _apiConnector;
        private readonly ILogger _log;

        public TranslationService(IAPIConnector apiConnector, ILogger  log)
        {
            _log = log;
            _apiConnector = apiConnector;
        }

        public async Task<TranslationData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));

            using var response = await _apiConnector
                .PostAsync(new Uri(TranslationURL), new TranslationRequest(text, source, target), cancellationToken)
                .ConfigureAwait(false);

            var translationDto = response.Content.Deserialize<TranslationResponse>();

            var validationResult = new TranslationResponseValidator(translationDto).Validate();
            if (!validationResult.IsValid)
            {
                _log?.Error(validationResult.Message);
                return null;
            }

            return translationDto.ToModel();
        }
    }
}
