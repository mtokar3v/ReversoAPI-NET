using ReversoAPI.Web.Clients.Interfaces;
using System.Threading.Tasks;
using System;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Tools.Parsers;
using ReversoAPI.Web.Http.Interfaces;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Values;

namespace ReversoAPI.Web.Clients
{
    public class SynonymsClient : APIClient, ISynonymsClient
    {
        private const string SynonimsURL = "https://synonyms.reverso.net/synonym/";

        private readonly IResponseParser<SynonymsData> _parser;

        public SynonymsClient(
            IAPIConnector apiConnector,
            IResponseParser<SynonymsData> parser) : base(apiConnector)
        {
            _parser = parser;
        }

        public async Task<SynonymsData> GetAsync(string text, Language language)
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
