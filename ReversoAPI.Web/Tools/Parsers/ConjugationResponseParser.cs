using HtmlAgilityPack;
using ReversoAPI.Web.Builders;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Exceptions;

namespace ReversoAPI.Web.Tools.Parsers
{
    public class ConjugationResponseParser : BaseResponseParser<ConjugationData>
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
