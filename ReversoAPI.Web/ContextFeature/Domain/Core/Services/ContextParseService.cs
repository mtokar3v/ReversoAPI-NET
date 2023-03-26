using System.IO;
using ReversoAPI.Web.ContextFeature.Domain.Supporting.Builders;
using ReversoAPI.Web.Shared.Domain.Services;
using ReversoAPI.Web.Shared.Infrastructure.Logger;

namespace ReversoAPI.Web.ContextFeature.Domain.Core.Services
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
