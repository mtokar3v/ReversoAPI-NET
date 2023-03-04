using System;
using System.Threading.Tasks;
using ReversoAPI.Web.Values;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.DTOs.TranslationObjects;
using System.Linq;
using System.Threading;

namespace ReversoAPI.Web.Clients
{
    public class TranslationClient : APIClient, ITranslationClient
    {
        private const string TranslationURL = "https://api.reverso.net/translate/v1/translation";
        private readonly static Language[] _supportedLanguades =
        {
            Language.Arabic,
            Language.Chinese,
            Language.Czech,
            Language.Danish,
            Language.Dutch,
            Language.French,
            Language.German,
            Language.Greek,
            Language.Hebrew,
            Language.Hindi,
            Language.Hungarian,
            Language.Italian,
            Language.Japanese,
            Language.Korean,
            Language.Persian,
            Language.Polish,
            Language.Portuguese,
            Language.Romanian,
            Language.Russian,
            Language.Slovak,
            Language.Spanish,
            Language.Swedish,
            Language.Thai,
            Language.Turkish,
            Language.Ukrainian,
            Language.English,
        };

        public TranslationClient(IAPIConnector apiConnector) : base(apiConnector) { }

        public async Task<TranslationData> GetAsync(string text, Language source, Language target, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(text)) return null;
            if (text.Length > 2000) throw new ArgumentException("The text provided exceeds the limit of 2000 symbols.");
            if (!_supportedLanguades.Contains(source)) throw new NotSupportedException($"'{source}' is not supported.");
            if (!_supportedLanguades.Contains(target)) throw new NotSupportedException($"'{target}' is not supported.");
            if (source == target) throw new ArgumentException("Source and target languages have the same value."); 

            using var response = await _apiConnector
                .PostAsync(new Uri(TranslationURL), new TranslationRequest(text, source, target), cancellationToken)
                .ConfigureAwait(false);

            var translationDto = response.Content.Deserialize<TranslationResponse>();

            var validationData = TranslationResponseValidator.IsValid(translationDto);
            if (!validationData.IsValid) return null;

            return translationDto.ToModel();
        }
    }
}
