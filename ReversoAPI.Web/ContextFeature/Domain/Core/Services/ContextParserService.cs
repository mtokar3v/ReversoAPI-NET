using System.IO;
using ReversoAPI.Web.ContextFeature.Domain.Supporting.Builders;
using ReversoAPI.Web.Shared.Domain.Services;

namespace ReversoAPI.Web.ContextFeature.Domain.Core.Services
{
    public class ContextParserService : BaseParser<ContextData>
    {
        protected override ContextData Parse(Stream htmlStream)
        {
            try
            {
                return new ContextParseBuilder(htmlStream)
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
