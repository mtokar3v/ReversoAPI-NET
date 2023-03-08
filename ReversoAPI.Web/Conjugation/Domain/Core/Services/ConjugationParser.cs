using HtmlAgilityPack;
using ReversoAPI.Web.Builders;
using ReversoAPI.Web.Conjugation.Domain.Core.Entities;
using ReversoAPI.Web.Shared.Domain.Exceptions;
using ReversoAPI.Web.Shared.Domain.Services;

namespace ReversoAPI.Web.Conjugation.Domain.Core.Services
{
    public class ConjugationParser : BaseParser<ConjugationData>
    {
        protected override ConjugationData Parse(HtmlDocument html)
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
