using System;
using System.Threading.Tasks;
using ReversoAPI.Web.Values;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Tools.Parsers;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Clients.Interfaces;
using System.Threading;
using System.Web;
using ReversoAPI.Web.Values.Validators;

namespace ReversoAPI.Web.Clients
{
    public class ConjugationClient : APIClient, IConjugationClient
    {
        private string ConjugationURL = "https://conjugator.reverso.net/conjugation-";

        private readonly IResponseParser<ConjugationData> _parser;

        public ConjugationClient(
            IAPIConnector apiConnector,
            IResponseParser<ConjugationData> parser) : base(apiConnector)
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
