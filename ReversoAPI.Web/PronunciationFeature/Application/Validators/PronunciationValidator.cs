using System;
using System.Linq;
using System.Collections.Generic;
using ReversoAPI.Web.Shared.Application.Interfaces;
using ReversoAPI.Web.Shared.Application.Validators;

namespace ReversoAPI.Web.PronunciationFeature.Application.Validators
{
    public class PronunciationValidator : AbstractValidator
    {
        private static Language[] _supportedLanguades =
        {
            Language.Arabic, Language.Spanish, Language.Hebrew,
            Language.Japanese, Language.Polish, Language.Romanian,
            Language.Turkish, Language.English, Language.German,
            Language.French, Language.Italian, Language.Dutch,
            Language.Portuguese, Language.Russian, Language.Chinese,
        };

        public PronunciationValidator(string text, Language language, int speed = 100)
        {
            Text = text;
            Language = language;
            Speed = speed;
        }

        public string Text { get; }
        public Language Language { get; }
        public int Speed { get; }

        protected override IEnumerable<Func<IValidationResult>> GetValidators()
        {
            Func<IValidationResult>[] validators =
            {
                () => ValidateText(Text),
                () => ValidateLanguage(Language),
                () => ValidateSpeed(Speed),
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

            if (text.Length > 1300)
            {
                var message = "The text provided exceeds the limit of 1300 symbols.";
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

        private IValidationResult ValidateSpeed(int speed)
        {
            if (speed < 30 || speed > 300)
            {
                var message = "Speed must be between 30 and 300.";
                return new ValidationResult(false, message, new ArgumentException(message, nameof(speed)));
            }

            return null;
        }

        #endregion
    }
}
