using HtmlAgilityPack;
using ReversoAPI.Web.Builders;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Exceptions;

namespace ReversoAPI.Web.Tools.Parsers
{
    public class ContextResponseParser : BaseResponseParser<ContextData>
    {
        protected override ContextData Parse(HtmlDocument html)
        {
            try
            {
                return new ContextParseBuilder(html)
                    .WithInputText()
                    .WithLanguages()
                    .WithExamples()
                    .Build();
            }
            catch(ParsingException)
            {
                return null;
            }
        }
    }
}
