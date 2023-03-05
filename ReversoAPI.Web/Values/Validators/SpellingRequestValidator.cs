﻿using System;
using System.Collections.Generic;
using System.Linq;
using ReversoAPI.Web.Extensions;

namespace ReversoAPI.Web.Values.Validators
{
    internal class SpellingRequestValidator : AbstractValidator
    {
        private static Language[] _supportedLanguades =
        {
            Language.English, Language.French,
            Language.Spanish, Language.Italian
        };

        public SpellingRequestValidator(string text, Language language, Locale locale = Locale.None)
        {
            Text = text;
            Language = language;
            Locale = locale;
        }

        public string Text { get; }
        public Language Language { get; }
        public Locale Locale { get; }

        public override IEnumerable<Func<IValidationResult>> GetValidators()
        {
            Func<IValidationResult>[] validators =
            {
                () => ValidateText(Text),
                () => ValidateLanguage(Language),
                () => ValidateLocale(Language, Locale),
            };

            return validators;
        }

        #region Private Methods

        private IValidationResult ValidateText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                var message = "Text cannot be null or empty.";
                return new ValidationResult(false, message, new ArgumentException(message, nameof(text)));
            }

            if (text.Length > 3090)
            {
                var message = "The text provided exceeds the limit of 3090 symbols.";
                return new ValidationResult(false, message, new ArgumentException(message, nameof(text)));
            }

            return null;
        }

        private IValidationResult ValidateLanguage(Language language)
        {
            if (!_supportedLanguades.Contains(language))
            {
                var message = $"'{language}' is not supported.";
                return new ValidationResult(false, message, new NotSupportedException(message));
            }

            return null;
        }

        private IValidationResult ValidateLocale(Language language, Locale locale)
        {
            if (locale != Locale.None && language != locale.GetLanguage())
            {
                var message = $"{language} does not support {locale} locale.";
                return new ValidationResult(false, message, new ArgumentException(message));
            }

            return null;
        }

        #endregion
    }
}
