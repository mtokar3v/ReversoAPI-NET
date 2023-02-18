using ReversoAPI.Web.Clients.Interfaces;
using ReversoAPI.Web.Models.Responses;
using ReversoAPI.Web.Models.Values;
using System.Threading.Tasks;
using System;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Tools.Parsers;
using ReversoAPI.Web.Http.Interfaces;

namespace ReversoAPI.Web.Clients
{
    public class SynonymsClient : APIClient, ISynonymsClient
    {
        private const string SynonimsURL = "https://synonyms.reverso.net/synonym/";

        private readonly IResponseParser<SynonymsResponse> _parser;

        public SynonymsClient(
            IAPIConnector apiConnector,
            IResponseParser<SynonymsResponse> parser) : base(apiConnector)
        {
            _parser = parser;
        }

        public async Task<SynonymsResponse> GetAsync(string text, Language language)
        {
            if (string.IsNullOrEmpty(text)) return null;

            var url = CombineUrl(text, language);

            var response = await _apiConnector.GetAsync(url);
            if (!response.IsHtml()) throw new FormatException("Response does not match html format");

            return _parser.Invoke(response.Content);
        }

        private Uri CombineUrl(string text, Language language) 
            => new Uri(SynonimsURL + $"{language.ToShortName()}/{text.Replace(' ', '+')}");
    }
}
