using System;
using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.Values;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Tools.Parsers;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.Values.Validators;

namespace ReversoAPI.Web.Clients
{
    public class ContextClient : APIClient, IContextClient
    {
        private const string ContextURL = "https://context.reverso.net/translation/";

        private readonly IResponseParser<ContextData> _parser;

        public ContextClient(
            IAPIConnector apiConnector,
            IResponseParser<ContextData> parser) : base(apiConnector)
        {
            _parser = parser;
        }

        public async Task<ContextData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default)
        {
            var validationResult = new ContextRequestValidator(text, source, target).Validate();
            if (!validationResult.IsValid) throw validationResult.Exception;

            var url = CombineUrl(text, source, target);

            using var response = await _apiConnector
                .GetAsync(url, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsHtml()) throw new FormatException("The server response contains data that is not in HTML format.");

            return _parser.Invoke(response.Content);
        }

        private Uri CombineUrl(string text, Language source, Language target)
        {
            var sourceLanguage = source.ToString().ToLower();
            var targetLanguage = target.ToString().ToLower();

            return new Uri(ContextURL + $"{sourceLanguage}-{targetLanguage}/{text}");
        }
    }
}
