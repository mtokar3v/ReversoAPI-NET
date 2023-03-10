using System;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.ConjugationFeature.Domain.Core.Interfaces.Entities;
using ReversoAPI.Web.ConjugationFeature.Application.Validators;
using ReversoAPI.Web.ConjugationFeature.Application.Interfaces.Services;
using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.Shared.Domain.Interfaces.Services;
using ReversoAPI.Web.Shared.Infrastructure.Http.Interfaces;

namespace ReversoAPI.Web.ConjugationFeature.Application.Services
{
    public class ConjugationService : IConjugationService
    {
        private string ConjugationURL = "https://conjugator.reverso.net/conjugation-";

        private readonly IAPIConnector _apiConnector;
        private readonly IParser<IConjugationData> _parser;

        public ConjugationService(
            IAPIConnector apiConnector,
            IParser<IConjugationData> parser)
        {
            _apiConnector = apiConnector;
            _parser = parser;
        }

        public async Task<IConjugationData> GetAsync(string text, Language language, CancellationToken cancellationToken = default)
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
