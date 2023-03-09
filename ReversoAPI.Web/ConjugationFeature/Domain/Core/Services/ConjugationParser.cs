using HtmlAgilityPack;
using ReversoAPI.Web.ConjugationFeature.Domain.Core.Interfaces.Entities;
using ReversoAPI.Web.ConjugationFeature.Domain.Supporting.Builders;
using ReversoAPI.Web.Shared.Domain.Exceptions;
using ReversoAPI.Web.Shared.Domain.Services;

namespace ReversoAPI.Web.ConjugationFeature.Domain.Core.Services
{
    public class ConjugationParser : BaseParser<IConjugationData>
    {
        protected override IConjugationData Parse(HtmlDocument html)
        {
            try
            {
                return new ConjugationParseBuilder(html)
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
