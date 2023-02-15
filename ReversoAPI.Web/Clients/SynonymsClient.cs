using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.Models.Responses;
using ReversoAPI.Web.Models.Values;
using System.Threading.Tasks;
using System;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Tools.Parsers;

namespace ReversoAPI.Web.Clients
{
    public class SynonymsClient : APIClient, ISynonimsClient
    {
        private const string SynonimsURL = "https://synonyms.reverso.net/synonym/";

        private readonly IResponseParser<SynonymsResponse> _parser;

        public SynonymsClient(IResponseParser<SynonymsResponse> parser)
        {
            _parser = parser;
        }

        public async Task<SynonymsResponse> GetAsync(string text, Language language)
        {
            if (string.IsNullOrEmpty(text)) return null;

            var url = CombineUrl(text, language);
            var response = await API.GetAsync(url);
            return _parser.Invoke(response);
        }

        private Uri CombineUrl(string text, Language language) 
            => new Uri(SynonimsURL + $"{language.ToShortName()}/{text.Replace(' ', '+')}");
    }
}
