using HtmlAgilityPack;
using ReversoAPI.Web.Builders;
using ReversoAPI.Web.Models.Responses;

namespace ReversoAPI.Web.Tools.Parsers
{
    public class ContextResponseParser : BaseResponseParser<ContextResponse>
    {
        protected override ContextResponse Parse(HtmlDocument html)
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
