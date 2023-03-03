using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.DTOs.TranslationObjects;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Values;
using System;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Clients
{
    public class TranslationClient : APIClient, ITranslationClient
    {
        private const string TranslationURL = "https://api.reverso.net/translate/v1/translation";

        public TranslationClient(IAPIConnector apiConnector) : base(apiConnector)
        {
        }

        public async Task<TranslationData> GetAsync(string text, Language source, Language target)
        {
            if (string.IsNullOrEmpty(text)) return null;
            if (source == target) throw new ArgumentException("Source and Target languages are similar"); // maybe should rid of this

            using var response = await _apiConnector
                .PostAsync(new Uri(TranslationURL), new TranslationRequest(text, source, target))
                .ConfigureAwait(false);

            var translationDto = response.Content.Deserialize<TranslationResponse>();

            return translationDto.ToModel();
        }
    }
}
