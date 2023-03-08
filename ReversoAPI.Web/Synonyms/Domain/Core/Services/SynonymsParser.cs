using HtmlAgilityPack;
using ReversoAPI.Web.Builders;
using ReversoAPI.Web.Shared.Domain.Services;
using ReversoAPI.Web.Synonyms.Domain.Core.Entities;

namespace ReversoAPI.Web.Synonyms.Domain.Core.Services
{
    public class SynonymsParser : BaseParser<SynonymsData>
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
