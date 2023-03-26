using System.IO;
using ReversoAPI.Web.ConjugationFeature.Domain.Supporting.Builders;
using ReversoAPI.Web.Shared.Domain.Services;

namespace ReversoAPI.Web
{
    public class ConjugationParseService : BaseParser<ConjugationData>
    {
        private readonly ILogger _log;

        public ConjugationParseService(ILogger log)
        {
            _log = log;
        }

        protected override ConjugationData Parse(Stream htmlStream)
        {
            try
            {
                return new ConjugationParseBuilder(htmlStream)
                    .WithInputText()
                    .WithLanguage()
                    .WithConjugations()
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
