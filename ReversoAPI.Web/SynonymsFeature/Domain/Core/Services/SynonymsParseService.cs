using System.IO;
using ReversoAPI.Web.SynonymsFeature.Domain.Supporting.Builders;
using ReversoAPI.Web.Shared.Domain.Services;

namespace ReversoAPI.Web
{
    public class SynonymsParseService : BaseParser<SynonymsData>
    {
        private readonly ILogger _log;

        public SynonymsParseService(ILogger log)
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
