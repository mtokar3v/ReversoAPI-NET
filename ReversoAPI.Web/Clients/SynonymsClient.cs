using System;
using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.Values;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Tools.Parsers;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.Values.Validators;

namespace ReversoAPI.Web.Clients
{
    public class SynonymsClient : APIClient, ISynonymsClient
    {
        private const string SynonimsURL = "https://synonyms.reverso.net/synonym/";

        private readonly IResponseParser<SynonymsData> _parser;

        public SynonymsClient(
            IAPIConnector apiConnector,
            IResponseParser<SynonymsData> parser) : base(apiConnector)
        {
            _parser = parser;
        }

        public async Task<SynonymsData> GetAsync(string text, Language language, CancellationToken cancellationToken = default)
        {
            var validationResult = new SynonymsRequestValidator(text, language).Validate();
            if (!validationResult.IsValid) throw validationResult.Exception;

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
