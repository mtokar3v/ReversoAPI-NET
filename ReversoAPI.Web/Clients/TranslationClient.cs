using System;
using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.Values;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.DTOs.TranslationObjects;
using ReversoAPI.Web.Values.Validators;

namespace ReversoAPI.Web.Clients
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
