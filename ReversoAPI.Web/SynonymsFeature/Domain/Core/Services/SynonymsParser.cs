using HtmlAgilityPack;
using ReversoAPI.Web.SynonymsFeature.Domain.Core.Interfaces.Entities;
using ReversoAPI.Web.SynonymsFeature.Domain.Supporting.Builders;
using ReversoAPI.Web.Shared.Domain.Services;

namespace ReversoAPI.Web.SynonymsFeature.Domain.Core.Services
{
    public class SynonymsParser : BaseParser<ISynonymsData>
    {
        protected override ISynonymsData Parse(HtmlDocument html)
        {
            return new SynonymsParseBuilder(html)
                .WithInputText()
                .WithLanguage()
                .WithSynonyms()
                .Build();
        }
    }
}
