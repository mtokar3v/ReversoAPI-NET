using System;
using System.Linq;
using System.Collections.Generic;

namespace ReversoAPI.Web.Values.Validators
{
    internal class TranslationRequestValidator : AbstractValidator
    {
        private readonly static Language[] _supportedLanguades =
        {
            Language.Arabic, Language.Chinese, Language.Czech,
            Language.Danish, Language.Dutch, Language.French,
            Language.German, Language.Greek, Language.Hebrew,
            Language.Hindi, Language.Hungarian, Language.Italian,
            Language.Japanese, Language.Korean, Language.Persian,
            Language.Polish, Language.Portuguese, Language.Romanian,
            Language.Russian, Language.Slovak, Language.Spanish,
            Language.Swedish, Language.Thai, Language.Turkish,
            Language.Ukrainian, Language.English,
        };

        public TranslationRequestValidator(string text, Language source, Language target)
        {
            Text = text;
            Source = source;
            Target = target;
        }

        public string Text { get; }
        public Language Source { get; }
        public Language Target { get; }

        public override IEnumerable<Func<IValidationResult>> GetValidators()
        {
            Func<IValidationResult>[] validators =
            {
                () => ValidateText(Text),
                () => ValidateLanguage(Source),
                () => ValidateLanguage(Target),
                () => ValidateLanguageСompatibility(Source, Target),
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

            if (text.Length > 2000)
            {
                var message = "The text provided exceeds the limit of 2000 symbols.";
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

        private IValidationResult ValidateLanguageСompatibility(Language source, Language target)
        {
            if (source == target)
            {
                var message = $"Source and target languages have the same value.";
                return new ValidationResult(false, message, new ArgumentException(message));
            }

            return null;
        }

        #endregion
    }
}
