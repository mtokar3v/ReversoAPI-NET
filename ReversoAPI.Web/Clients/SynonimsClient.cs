using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.Models.Responses;
using ReversoAPI.Web.Models.Values;
using System.Threading.Tasks;
using System;
using ReversoAPI.Web.Extensions;

namespace ReversoAPI.Web.Clients
{
    public class SynonimsClient : APIClient, ISynonimsClient
    {
        private const string SynonimsURL = "https://synonyms.reverso.net/synonym/";
        public Task<SynonymsResponse> GetAsync(string text, Language language)
        {
            if (string.IsNullOrEmpty(text)) return null;

            var url = CombineUrl(text, language);
            return API.GetAsync<SynonymsResponse>(url);
        }

        private Uri CombineUrl(string text, Language language) 
            => new Uri(SynonimsURL + $"{language.ToShortName()}/{text.Replace(' ', '+')}");
    }
}
