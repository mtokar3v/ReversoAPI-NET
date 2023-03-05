using System;
using System.Collections.Generic;
using System.Linq;

namespace ReversoAPI.Web.Values.Validators
{
    internal class SynonymsRequestValidator : AbstractValidator
    {
        private readonly static Language[] _supportedLanguades =
        {
            Language.Arabic, Language.German, Language.Spanish,
            Language.French, Language.Hebrew, Language.Italian,
            Language.Japanese, Language.Korean, Language.Dutch,
            Language.Polish, Language.Portuguese, Language.Romanian,
            Language.Russian,
        };

        public SynonymsRequestValidator(string text, Language language)
        {
            Text = text;
            Language = language;
        }

        public string Text { get; }
        public Language Language { get; }

        public override IEnumerable<Func<IValidationResult>> GetValidators()
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

            if (text.Length > 200)
            {
                var message = "The text provided exceeds the limit of 200 symbols.";
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
