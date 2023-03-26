using System;
using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.SynonymsFeature.Application.Interfaces.Services;
using ReversoAPI.Web.Shared.Domain.Extensions;
using ReversoAPI.Web.Shared.Domain.Interfaces.Services;
using ReversoAPI.Web.Shared.Infrastructure.Http.Interfaces;

namespace ReversoAPI.Web.SynonymsFeature.Application.Services
{
    public class SynonymsService : ISynonymsService
    {
        private const string SynonimsURL = "https://synonyms.reverso.net/synonym/";

        private readonly IAPIConnector _apiConnector;
        private readonly IParseService<SynonymsData> _parser;

        public SynonymsService(
            IAPIConnector apiConnector,
            IParseService<SynonymsData> parser)
        {
            _apiConnector = apiConnector;
            _parser = parser;
        }

        public async Task<SynonymsData> GetAsync(string text, Language language, CancellationToken cancellationToken = default)
        {
            var url = CombineUrl(text, language);

            using var response = await _apiConnector
                .GetAsync(url, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsHtml()) throw new FormatException("The server response contains data that is not in HTML format.");

            return _parser.Invoke(response.Content);
        }

        private Uri CombineUrl(string text, Language language)
            => new Uri(SynonimsURL + $"{language.ToShortName()}/{text.Replace(' ', '+')}");
    }
}
