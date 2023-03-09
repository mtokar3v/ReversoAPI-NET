using System;
using System.Linq;
using System.Collections.Generic;
using ReversoAPI.Web.GrammarCheckFeature.Domain.Entities;
using ReversoAPI.Web.GrammarCheckFeature.Domain.Interfaces.Entities;
using ReversoAPI.Web.GrammarCheckFeature.Application.DTOs;
using ReversoAPI.Web.TranslationFeature.Domain.Entities;
using ReversoAPI.Web.TranslationFeature.Domain.Interfaces.Entities;
using ReversoAPI.Web.TranslationFeature.Domain.Interfaces.ValueObjects;
using ReversoAPI.Web.TranslationFeature.Domain.ValueObjects;
using ReversoAPI.Web.TranslationFeature.Application.DTOs;
using ReversoAPI.Web.Shared.Domain.Extensions;

namespace ReversoAPI.Web.Shared.Application.Extensions
{
    public static class DtoExtensions
    {
        public static SpellingData ToModel(this SpellingResponse spellingDto)
        {
            if (spellingDto == null) return null;

            return new SpellingData
            {
                Text = spellingDto.Text,
                Language = spellingDto.Language.ToLanguageFromMediumName(),
                Correction = spellingDto.Corrections.Select(c => c.ToModel()),
            };
        }

        public static ICorrection ToModel(this CorrectionDto correctionDto)
        {
            if (correctionDto == null) return null;

            return new Correction(
                correctionDto.CorrectionText,
                correctionDto.MistakeText,
                correctionDto.StartIndex,
                correctionDto.EndIndex,
                correctionDto.ShortDescription,
                correctionDto.LongDescription,
                correctionDto.Suggestions.Select(s => s.Text));
        }

        public static ITranslationData ToModel(this TranslationResponse translationDto)
        {
            if (translationDto == null) return null;

            var hasExtraFields = !string.IsNullOrWhiteSpace(translationDto.ContextResults?.Results?.FirstOrDefault()?.Translation);

            return new TranslationData
            {
                Text = string.Join(Environment.NewLine, translationDto.Input.Select(t => t.ReplaceSpecSymbols())),
                Source = translationDto.From.ToLanguageFromMediumName(),
                Target = translationDto.To.ToLanguageFromMediumName(),
                Translations = hasExtraFields ? GetFullTranslations() : GetShortTranslations(),
            };

            IEnumerable<ITranslation> GetShortTranslations()
            {
                var originalText = string.Join(Environment.NewLine, translationDto.Input.Select(t => t.ReplaceSpecSymbols()));
                var translatedText = string.Join(Environment.NewLine, translationDto.Translation.Select(t => t.ReplaceSpecSymbols()));

                var sourceLanguage = translationDto.From.ToLanguageFromMediumName();
                var targetLanguage = translationDto.To.ToLanguageFromMediumName();

                var translation = new Translation(originalText, translatedText, sourceLanguage, targetLanguage);
                return new Translation[] { translation };
            }

            IEnumerable<ITranslation> GetFullTranslations()
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
