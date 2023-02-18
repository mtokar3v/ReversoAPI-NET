using ReversoAPI.Web.DTOs.SpellingResponseData;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Values;
using System.Linq;

namespace ReversoAPI.Web.Extensions
{
    public static class DtoExtensions
    {
        public static SpellingData ToModel(this SpellingResponse spellingDto)
        {
            return new SpellingData
            {
                Text = spellingDto.Text,
                Language = spellingDto.Language.ToLanguageFromMediumName(),

                // AutoMapper?
                Correction = spellingDto.Corrections.Select(c => new Correction(c.CorrectionText,
                                                                                c.MistakeText,
                                                                                c.StartIndex,
                                                                                c.EndIndex,
                                                                                c.ShortDescription,
                                                                                c.LongDescription)),
            };
        }
    }
}
