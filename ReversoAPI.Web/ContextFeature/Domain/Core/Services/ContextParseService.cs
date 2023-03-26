using System.IO;
using ReversoAPI.Web.ContextFeature.Domain.Supporting.Builders;
using ReversoAPI.Web.Shared.Domain.Services;

namespace ReversoAPI.Web
{
    public class ContextParseService : BaseParser<ContextData>
    {
        private readonly ILogger _log;
        public ContextParseService(ILogger log)
        {
            _log = log;
        }

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
            catch (ParsingException ex)
            {
                _log?.Error(ex.Message);
                return null;
            }
        }
    }
}
