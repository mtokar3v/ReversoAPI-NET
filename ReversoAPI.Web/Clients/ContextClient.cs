using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.Models.Responses;
using ReversoAPI.Web.Models.Values;
using System;
using System.Threading.Tasks;

namespace ReversoAPI.Web.Clients
{
    public class ContextClient : APIClient, IContextClient
    {
        private const string ContextURL = "https://context.reverso.net/translation/";
        public Task<ContextResponse> GetAsync(string text, Language source, Language target)
        {
            if (string.IsNullOrEmpty(text)) return null;
            if (source == target) throw new ArgumentException("Source and Target languages are similar"); // maybe should rid of this

            var url = CombineUrl(text, source, target);
            return API.GetAsync<ContextResponse>(url);
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
