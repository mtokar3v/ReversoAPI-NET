using System;
using System.Linq;
using System.Collections.Generic;
using ReversoAPI.Web.Shared.Application.Interfaces;
using ReversoAPI.Web.Shared.Application.Validators;

namespace ReversoAPI.Web.ConjugationFeature.Application.Validators
{
    public class ConjugationRequestValidator : AbstractValidator
    {
        private static Language[] _supportedLanguades =
        {
            Language.English, Language.French,
            Language.Spanish, Language.German,
            Language.Italian, Language.Portuguese,
            Language.Russian,
            //Language.Hebrew, 
            //Language.Arabic,
            //Language.Japanese,
        };

        public ConjugationRequestValidator(string text, Language language)
        {
            Text = text;
            Language = language;
        }

        public string Text { get; }
        public Language Language { get; }

        protected override IEnumerable<Func<IValidationResult>> GetValidators()
        {
            Func<IValidationResult>[] validators =
            {
                () => ValidateText(Text),
                () => ValidateLanguage(Language),
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

            if (text.Length > 300)
            {
                var message = "The text provided exceeds the limit of 300 symbols.";
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

        #endregion
    }
}
