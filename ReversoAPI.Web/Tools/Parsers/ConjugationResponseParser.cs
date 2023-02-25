using HtmlAgilityPack;
using ReversoAPI.Web.Builders;
using ReversoAPI.Web.Entities;

namespace ReversoAPI.Web.Tools.Parsers
{
    public class ConjugationResponseParser : BaseResponseParser<ConjugationData>
    {
        protected override ConjugationData Parse(HtmlDocument html)
        { 
            return new ConjugationParseBuilder(html)
                .WithLanguage()
                .WithConjugations()
                .Build();
        }
    }
}
