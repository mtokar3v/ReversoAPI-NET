using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Tools.Parsers;
using ReversoAPI.Web.Values;
using System;
using System.Threading.Tasks;

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

        public async Task<ContextData> GetAsync(string text, Language source, Language target)
        {
            if (string.IsNullOrEmpty(text)) return null;
            if (source == target) throw new ArgumentException("Source and Target languages are similar"); // maybe should rid of this

            var url = CombineUrl(text, source, target);

            var response = await _apiConnector.GetAsync(url);
            if (!response.IsHtml()) throw new FormatException("Response does not match html format");

            return _parser.Invoke(response.Content);
        }

        private Uri CombineUrl(string text, Language source, Language target)
        {
            // add text validation to decline spec symbols
            var sourceLanguage = source.ToString().ToLower();
            var targetLanguage = target.ToString().ToLower();

            return new Uri(ContextURL + $"{sourceLanguage}-{targetLanguage}/{text}");
        }
    }
}
