using HtmlAgilityPack;
using ReversoAPI.Web.ContextFeature.Domain.Core.Interfaces.Entities;
using ReversoAPI.Web.ContextFeature.Domain.Supporting.Builders;
using ReversoAPI.Web.Shared.Domain.Exceptions;
using ReversoAPI.Web.Shared.Domain.Services;

namespace ReversoAPI.Web.ContextFeature.Domain.Core.Services
{
    public class ContextParser : BaseParser<IContextData>
    {
        protected override IContextData Parse(HtmlDocument html)
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
