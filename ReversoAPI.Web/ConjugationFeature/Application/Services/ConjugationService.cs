using System;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.ConjugationFeature.Application.Interfaces.Services;

namespace ReversoAPI.Web.ConjugationFeature.Application.Services
{
    public class ConjugationService : IConjugationService
    {
        private string ConjugationURL = "https://conjugator.reverso.net/conjugation-";

        private readonly IAPIConnector _apiConnector;
        private readonly IParseService<ConjugationData> _parser;

        public ConjugationService(
            IAPIConnector apiConnector,
            IParseService<ConjugationData> parser)
        {
            _apiConnector = apiConnector;
            _parser = parser;
        }

        public async Task<ConjugationData> GetAsync(string text, Language language, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(text)) return null;

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
