using System;
using System.Linq;
using System.Collections.Generic;
using ReversoAPI.Web.Shared.Application.Validators;
using ReversoAPI.Web.Shared.Application.Interfaces;

namespace ReversoAPI.Web.ContextFeature.Application.Validators
{
    public class ContextRequestValidator : AbstractValidator
    {
        private readonly static Language[] _supportedLanguages =
        {
            Language.Arabic, Language.German, Language.Spanish,
            Language.French, Language.Hebrew, Language.Italian,
            Language.Japanese, Language.Korean, Language.Dutch,
            Language.Polish, Language.Portuguese, Language.Romanian,
            Language.Russian, Language.Swedish, Language.Turkish,
            Language.Ukrainian, Language.Chinese, Language.Swedish,
            Language.English,
        };

        public ContextRequestValidator(string text, Language source, Language target)
        {
            Text = text;
            Source = source;
            Target = target;
        }

        public string Text { get; }
        public Language Source { get; }
        public Language Target { get; }

        protected override IEnumerable<Func<IValidationResult>> GetValidators()
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

            if (text.Length > 6000)
            {
                var message = "The text provided exceeds the limit of 6000 symbols.";
                return new ValidationResult(false, message, new ArgumentException(message, nameof(text)));
            }

            return null;
        }

        private IValidationResult ValidateLanguage(Language language)
        {
            if (!_supportedLanguages.Contains(language))
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
