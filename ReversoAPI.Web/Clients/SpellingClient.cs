using System;
using System.Threading.Tasks;
using ReversoAPI.Web.Values;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.DTOs.SpellingResponseData;
using System.Threading;
using ReversoAPI.Web.Values.Validators;

namespace ReversoAPI.Web.Clients
{
    public class SpellingClient : APIClient, ISpellingClient
    {
        private const string SpellingURL = "https://orthographe.reverso.net/api/v1/Spelling/";

        public SpellingClient(IAPIConnector apiConnector) : base(apiConnector) { }

        public async Task<SpellingData> GetAsync(string text, Language language, Locale locale = Locale.None, CancellationToken cancellationToken = default)
        {
            var validationResult = new SpellingRequestValidator(text, language, locale).Validate();
            if(!validationResult.IsValid) throw validationResult.Exception;

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
