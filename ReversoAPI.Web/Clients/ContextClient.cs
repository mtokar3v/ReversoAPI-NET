using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ReversoAPI.Web.Values;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Tools.Parsers;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Clients.Interfaces;

namespace ReversoAPI.Web.Clients
{
    public class ContextClient : APIClient, IContextClient
    {
        private const string ContextURL = "https://context.reverso.net/translation/";
        private readonly static Language[] _supportedLanguades =
        {
            Language.Arabic,
            Language.German,
            Language.Spanish,
            Language.French,
            Language.Hebrew,
            Language.Italian,
            Language.Japanese,
            Language.Korean,
            Language.Dutch,
            Language.Polish,
            Language.Portuguese,
            Language.Romanian,
            Language.Russian,
            Language.Swedish,
            Language.Turkish,
            Language.Ukrainian,
            Language.Chinese,
            Language.Swedish,
            Language.English,
        };

        private readonly IResponseParser<ContextData> _parser;

        public ContextClient(
            IAPIConnector apiConnector,
            IResponseParser<ContextData> parser) : base(apiConnector)
        {
            _parser = parser;
        }

        public async Task<ContextData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(text)) return null;
            if (text.Length > 6000) throw new ArgumentException("The text provided exceeds the limit of 6000 symbols.");
            if (!_supportedLanguades.Contains(source)) throw new NotSupportedException($"'{source}' is not supported");
            if (!_supportedLanguades.Contains(target)) throw new NotSupportedException($"'{target}' is not supported");
            if (source == target) throw new ArgumentException("Source and target languages have the same value.");

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
