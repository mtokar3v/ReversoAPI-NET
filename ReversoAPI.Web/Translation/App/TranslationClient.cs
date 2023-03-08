using System;
using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.Values.Validators;
using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.Translation.App.DTOs;
using ReversoAPI.Web.Clients;

namespace ReversoAPI.Web.Translation.App
{
    public class TranslationClient : APIClient, ITranslationClient
    {
        private const string TranslationURL = "https://api.reverso.net/translate/v1/translation";

        public TranslationClient(IAPIConnector apiConnector) : base(apiConnector) { }

        public async Task<TranslationData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default)
        {
            var validationResult = new TranslationRequestValidator(text, source, target).Validate();
            if (!validationResult.IsValid) throw validationResult.Exception;

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
