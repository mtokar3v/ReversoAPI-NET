﻿using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.TranslationFeature.Application.Interfaces.Services;
using ReversoAPI.Web.TranslationFeature.Application.Validators;

namespace ReversoAPI
{
    public class TranslationClient : ITranslationClient
    {
        private readonly ITranslationService _translationService;

        public TranslationClient(ITranslationService translationService) => _translationService = translationService;

        public async Task<TranslationData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default)
        {
            var validationResult = new TranslationRequestValidator(text, source, target).Validate();
            if (!validationResult.IsValid) throw validationResult.Exception;

            var output = await _translationService
                .GetAsync(text, source, target, cancellationToken)
                .ConfigureAwait(false);

            return output;
        }
    }
}
