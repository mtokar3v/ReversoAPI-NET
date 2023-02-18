using HtmlAgilityPack;
using ReversoAPI.Web.Builders;
using ReversoAPI.Web.Entities;

namespace ReversoAPI.Web.Tools.Parsers
{
    public class SynonymsResponseParser : BaseResponseParser<SynonymsData>
    {
        protected override SynonymsData Parse(HtmlDocument html)
        {
            return new SynonymsParseBuilder(html)
                .WithInputText()
                .WithLanguage()
                .WithSynonyms()
                .Build();
        }
    }
}
