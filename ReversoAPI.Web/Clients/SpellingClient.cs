using System;
using System.Linq;
using System.Threading.Tasks;
using ReversoAPI.Web.Values;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.DTOs.SpellingResponseData;
using System.Threading;

namespace ReversoAPI.Web.Clients
{
    public class SpellingClient : APIClient, ISpellingClient
    {
        private const string SpellingURL = "https://orthographe.reverso.net/api/v1/Spelling/";

        private static Language[] _supportedLanguades =
        {
            Language.English,
            Language.French,
            Language.Spanish,
            Language.Italian
        };

        public SpellingClient(IAPIConnector apiConnector) : base(apiConnector)
        {
        }

        public async Task<SpellingData> GetAsync(string text, Language language, Locale locale = Locale.None, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(text)) return null;
            if (text.Length > 3090) throw new ArgumentException("The text provided exceeds the limit of 3090 symbols.");
            if (!_supportedLanguades.Contains(language)) throw new NotSupportedException($"'{language}' is not supported");
            if (language != locale.GetLanguage()) throw new ArgumentException($"{language} does not support {locale} locale");

            using var response = await _apiConnector
                .PostAsync(new Uri(SpellingURL), new SpellingRequest(text, language, locale), cancellationToken)
                .ConfigureAwait(false);

            var spellingDto = response.Content.Deserialize<SpellingResponse>();

            return spellingDto.ToModel();
        }
    }
}
