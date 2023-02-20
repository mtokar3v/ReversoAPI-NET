using ReversoAPI.Web.Http.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using System;
using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.Values;
using ReversoAPI.Web.DTOs.SpellingResponseData;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Entities;

namespace ReversoAPI.Web.Clients
{
    public class SpellingClient : APIClient, ISpellingClient
    {
        private const string SpellingURL = "https://orthographe.reverso.net/api/v1/Spelling/";

        private static Language[] SupportedLanguades =
        {
            Language.English,
            Language.French,
            Language.Spanish,
            Language.Italian
        };

        public SpellingClient(IAPIConnector apiConnector) : base(apiConnector)
        {
        }

        public async Task<SpellingData> GetAsync(string text, Language language, Locale locale = Locale.None)
        {
            if (!SupportedLanguades.Contains(language)) throw new NotSupportedException($"'{language}' is not supported");
            // TO DO: Add locale validation

            using var response = await _apiConnector.PostAsync(new Uri(SpellingURL), new SpellingRequest(text, language, locale));
            var spellingDto = response.Content.Deserialize<SpellingResponse>();

            return spellingDto.ToModel();
        }
    }
}
