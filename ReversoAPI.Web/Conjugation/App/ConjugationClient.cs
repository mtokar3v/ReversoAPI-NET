using System;
using System.Threading.Tasks;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Http.Interfaces;
using System.Threading;
using System.Web;
using ReversoAPI.Web.Values.Validators;
using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.Shared.Domain.Interfaces.Services;
using ReversoAPI.Web.Clients;
using ReversoAPI.Web.Conjugation.App.Interfaces;

namespace ReversoAPI.Web.Conjugation.App
{
    public class ConjugationClient : APIClient, IConjugationClient
    {
        private string ConjugationURL = "https://conjugator.reverso.net/conjugation-";

        private readonly IParser<ConjugationData> _parser;

        public ConjugationClient(
            IAPIConnector apiConnector,
            IParser<ConjugationData> parser) : base(apiConnector)
        {
            _parser = parser;
        }

        public async Task<ConjugationData> GetAsync(string text, Language language, CancellationToken cancellationToken = default)
        {
            var validationResult = new ConjugationRequestValidator(text, language).Validate();
            if (!validationResult.IsValid) throw validationResult.Exception;

            var url = CombineUrl(text, language);

            using var response = await _apiConnector
                .GetAsync(url, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsHtml()) throw new FormatException("The server response contains data that is not in HTML format.");

            return _parser.Invoke(response.Content);
        }

        private Uri CombineUrl(string text, Language language)
            => new Uri(ConjugationURL + $"{language}-verb-{HttpUtility.UrlEncode(text)}.html");
    }
}
