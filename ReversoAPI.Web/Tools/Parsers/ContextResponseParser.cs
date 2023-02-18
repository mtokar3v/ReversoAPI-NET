using HtmlAgilityPack;
using ReversoAPI.Web.Builders;
using ReversoAPI.Web.Entities;

namespace ReversoAPI.Web.Tools.Parsers
{
    public class ContextResponseParser : BaseResponseParser<ContextData>
    {
        protected override ContextData Parse(HtmlDocument html)
        {
            return new ContextParseBuilder(html)
                .WithInputText()
                .WithLanguages()
                .WithTranslations()
                .WithExamples()
                .Build();
        }
    }
}
