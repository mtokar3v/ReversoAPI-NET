using System.IO;
using ReversoAPI.Web.SynonymsFeature.Domain.Supporting.Builders;
using ReversoAPI.Web.Shared.Domain.Services;
using ReversoAPI.Web.Shared.Infrastructure.Logger;

namespace ReversoAPI.Web.SynonymsFeature.Domain.Core.Services
{
    public class SynonymsParserService : BaseParser<SynonymsData>
    {
        private readonly ILogger _log;

        public SynonymsParserService(ILogger log)
        {
            _log = log;
        }

        protected override SynonymsData Parse(Stream htmlStream)
        {
            try
            {
                return new SynonymsParseBuilder(htmlStream)
                    .WithInputText()
                    .WithLanguage()
                    .WithSynonyms()
                    .Build();
            }
            catch(ParsingException ex)
            {
                _log?.Error(ex.Message);
                return null;
            }
        }
    }
}
