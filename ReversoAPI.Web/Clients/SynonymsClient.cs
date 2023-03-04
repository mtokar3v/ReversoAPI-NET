using System;
using System.Linq;
using System.Threading.Tasks;
using ReversoAPI.Web.Values;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Tools.Parsers;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Clients.Interfaces;
using System.Threading;

namespace ReversoAPI.Web.Clients
{
    public class SynonymsClient : APIClient, ISynonymsClient
    {
        private const string SynonimsURL = "https://synonyms.reverso.net/synonym/";
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
        };

        private readonly IResponseParser<SynonymsData> _parser;

        public SynonymsClient(
            IAPIConnector apiConnector,
            IResponseParser<SynonymsData> parser) : base(apiConnector)
        {
            _parser = parser;
        }

        public async Task<SynonymsData> GetAsync(string text, Language language, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(text)) return null;
            if (text.Length > 200) throw new ArgumentException("The text provided exceeds the limit of 200 symbols.");
            if (!_supportedLanguades.Contains(language)) throw new NotSupportedException($"'{language}' is not supported");

            var url = CombineUrl(text, language);

            using var response = await _apiConnector
                .GetAsync(url, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsHtml()) throw new FormatException("Response does not match html format");

            return _parser.Invoke(response.Content);
        }

        private Uri CombineUrl(string text, Language language) 
            => new Uri(SynonimsURL + $"{language.ToShortName()}/{text.Replace(' ', '+')}");
    }
}
