using HtmlAgilityPack;
using ReversoAPI.Web.Builders;
using ReversoAPI.Web.Context.Domain.Core.Enities;
using ReversoAPI.Web.Exceptions;
using ReversoAPI.Web.Shared.Domain.Services;

namespace ReversoAPI.Web.Context.Domain.Core.Services
{
    public class ContextParser : BaseParser<ContextData>
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
            catch (ParsingException)
            {
                return null;
            }
        }
    }
}
