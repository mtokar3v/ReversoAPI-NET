using ReversoAPI.Web.ConjugationFeature.Domain.Core.Entities;
using ReversoAPI.Web.ConjugationFeature.Domain.Supporting.Builders;
using ReversoAPI.Web.Shared.Domain.Exceptions;
using ReversoAPI.Web.Shared.Domain.Services;
using System.IO;

namespace ReversoAPI.Web.ConjugationFeature.Domain.Core.Services
{
    public class ConjugationParser : BaseParser<ConjugationData>
    {
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
            catch (ParsingException)
            {
                return null;
            }
        }
    }
}
