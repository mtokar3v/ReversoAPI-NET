using System.IO;
using ReversoAPI.Web.ConjugationFeature.Domain.Supporting.Builders;
using ReversoAPI.Web.Shared.Domain.Services;

namespace ReversoAPI.Web.ConjugationFeature.Domain.Core.Services
{
    public class ConjugationParserService : BaseParser<ConjugationData>
    {
        protected override ConjugationData Parse(Stream htmlStream)
        {
            try
            {
                return new ConjugationParseBuilder(htmlStream)
                    .WithInputText()
                    .WithLanguage()
                    .WithConjugations()
                    .Build();
            }
            catch (ParsingException)
            {
                return null;
            }
        }
    }
}
