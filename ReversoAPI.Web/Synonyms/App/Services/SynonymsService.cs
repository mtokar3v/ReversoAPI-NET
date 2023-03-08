using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Shared.Domain.Interfaces.Services;
using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.Values.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ReversoAPI.Web.Synonyms.App.Services
{
    internal class SynonymsService : ISynonymsService
    {
        private const string SynonimsURL = "https://synonyms.reverso.net/synonym/";

        private readonly IAPIConnector _apiConnector;
        private readonly IParser<SynonymsData> _parser;

        public SynonymsService(
            IAPIConnector apiConnector,
            IParser<SynonymsData> parser)
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
