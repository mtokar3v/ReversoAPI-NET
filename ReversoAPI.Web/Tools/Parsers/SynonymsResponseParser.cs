using HtmlAgilityPack;
using ReversoAPI.Web.Builders;
using ReversoAPI.Web.Models.Responses;

namespace ReversoAPI.Web.Tools.Parsers
{
    public class SynonymsResponseParser : BaseResponseParser<SynonymsResponse>
    {
        protected override SynonymsResponse Parse(HtmlDocument html)
        {
            return new SynonymsParseBuilder(html)
                .WithInputText()
                .WithLanguage()
                .WithSynonyms()
                .Build();
        }
    }
}
