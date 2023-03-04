using System;
using System.Linq;
using System.Threading.Tasks;
using ReversoAPI.Web.Values;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Tools.Parsers;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Clients.Interfaces;
using System.Threading;
using System.Web;

namespace ReversoAPI.Web.Clients
{
    public class ConjugationClient : APIClient, IConjugationClient
    {
        private string ConjugationURL = "https://conjugator.reverso.net/conjugation-";
        private static Language[] _supportedLanguades =
        {
            Language.English,
            Language.French,
            Language.Spanish,
            Language.German,
            Language.Italian,
            Language.Portuguese,
            //Language.Hebrew, 
            Language.Russian,
            //Language.Arabic,
            //Language.Japanese,
        };

        private readonly IResponseParser<ConjugationData> _parser;

        public ConjugationClient(
            IAPIConnector apiConnector,
            IResponseParser<ConjugationData> parser) : base(apiConnector)
        {
            _parser = parser;
        }

        public async Task<ConjugationData> GetAsync(string text, Language language, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(text)) return null;
            if (text.Length > 300) throw new ArgumentException("The text provided exceeds the limit of 300 symbols.");
            if (!_supportedLanguades.Contains(language)) throw new NotSupportedException($"'{language}' is not supported");

            var url = CombineUrl(text, language);

            using var response = await _apiConnector
                .GetAsync(url, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsHtml()) throw new FormatException("Response does not match html format");

            return _parser.Invoke(response.Content);
        }

        private Uri CombineUrl(string text, Language language)
            => new Uri(ConjugationURL + $"{language}-verb-{HttpUtility.UrlEncode(text)}.html");
    }
}
