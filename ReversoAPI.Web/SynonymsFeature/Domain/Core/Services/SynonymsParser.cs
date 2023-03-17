using System.IO;
using ReversoAPI.Web.SynonymsFeature.Domain.Supporting.Builders;
using ReversoAPI.Web.Shared.Domain.Services;

namespace ReversoAPI.Web.SynonymsFeature.Domain.Core.Services
{
    public class SynonymsParser : BaseParser<SynonymsData>
    {
        protected override SynonymsData Parse(Stream htmlStream)
        {
            return new SynonymsParseBuilder(htmlStream)
                .WithInputText()
                .WithLanguage()
                .WithSynonyms()
                .Build();
        }
    }
}
