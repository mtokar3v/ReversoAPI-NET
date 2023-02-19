using ReversoAPI.Web.DTOs.SpellingResponseData;
using ReversoAPI.Web.DTOs.TranslationObjects;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Values;
using System;
using System.Collections.Generic;
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
                Correction = spellingDto.Corrections.Select(c => c.ToModel()),
            };
        }

        public static Correction ToModel(this CorrectionDto correctionDto) 
        {
            return new Correction(correctionDto.CorrectionText, correctionDto.MistakeText)
            {
                StartIndex = correctionDto.StartIndex,
                EndIndex = correctionDto.EndIndex,
                ShortDescription = correctionDto.ShortDescription,
                LongDescription = correctionDto.LongDescription,
                Suggestions = correctionDto.Suggestions.Select(s => s.Text),
            };
        }

        public static TranslationData ToModel(this TranslationResponse translationDto) 
        {
            var hasFullTranslation = !string.IsNullOrWhiteSpace(translationDto.ContextResults?.Results?.FirstOrDefault()?.Translation);

            return new TranslationData
            {
                Text = string.Join(Environment.NewLine, translationDto.Input.Select(t => t.ReplaceSpecSymbols())),
                Source = translationDto.From.ToLanguageFromMediumName(),
                Target = translationDto.To.ToLanguageFromMediumName(),
                Translations = hasFullTranslation ? GetFullTranslations() : GetShortTranslations(),
            };

            // TO DO: Refactor this

            IEnumerable<Translation> GetShortTranslations()
            {
                var originalText = string.Join(Environment.NewLine, translationDto.Input.Select(t => t.ReplaceSpecSymbols()));
                var translatedText = string.Join(Environment.NewLine, translationDto.Translation.Select(t => t.ReplaceSpecSymbols()));

                var sourceLanguage = translationDto.From.ToLanguageFromMediumName();
                var targetLanguage = translationDto.To.ToLanguageFromMediumName();

                var translation = new Translation(originalText, translatedText, sourceLanguage, targetLanguage);
                return new Translation[] { translation };
            }

            IEnumerable<Translation> GetFullTranslations()
            {
                var originalText = string.Join(Environment.NewLine, translationDto.Input.Select(t => t.ReplaceSpecSymbols()));

                var sourceLanguage = translationDto.From.ToLanguageFromMediumName();
                var targetLanguage = translationDto.To.ToLanguageFromMediumName();

                return translationDto.ContextResults.Results.Select(r => new Translation(originalText,
                                                                                  r.Translation,
                                                                                  sourceLanguage,
                                                                                  targetLanguage,
                                                                                  r.Transliteration,
                                                                                  r.PartOfSpeech.ToPartOfSpeech(),
                                                                                  r.Frequency,
                                                                                  r.Colloquial,
                                                                                  r.Rude));
            }
        }


    }
}
