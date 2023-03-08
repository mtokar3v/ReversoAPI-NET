using System;
using System.Threading.Tasks;
using System.Threading;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Tools.Parsers;
using ReversoAPI.Web.Domain.Core.Context.Services;
using ReversoAPI.Web.Domain.Core.Context.Enities;
using ReversoAPI.Web.Domain.Generic.ValueObjects;
using ReversoAPI.Web.Context.App.Interfaces;

namespace ReversoAPI.Web.Context.App.Services
{
    internal class ContextService : IContextService
    {
        private const string ContextURL = "https://context.reverso.net/translation/";

        private readonly IAPIConnector _apiConnector;
        private readonly IParser<ContextData> _parser;

        public ContextService(IAPIConnector apiConnector,
            IParser<ContextData> parser)
        {
            _apiConnector = apiConnector;
            _parser = parser;
        }

        public async Task<ContextData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default)
        {
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
