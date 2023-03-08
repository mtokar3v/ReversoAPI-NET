using System;
using System.Collections.Generic;
using ReversoAPI.Web.GrammarCheck.App.DTOs;
using ReversoAPI.Web.Shared.App.Interfaces;
using ReversoAPI.Web.Shared.App.Validators;

namespace ReversoAPI.Web.GrammarCheck.App.Validators
{
    internal class SpellingResponseValidator : AbstractValidator
    {
        public SpellingResponseValidator(SpellingResponse response)
        {
            Response = response;
        }

        public SpellingResponse Response { get; }

        public override IEnumerable<Func<IValidationResult>> GetValidators()
        {
            Func<IValidationResult>[] validators =
            {
                () => ValidateText(Response.Text),
                () => ValidateLanguage(Response.Language),
                () => ValidateCorrections(Response.Corrections),
            };

            return validators;
        }

        #region Private Methods

        private IValidationResult ValidateText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                var message = "Received text is null or empty.";
                return new ValidationResult(false, message, new NullReferenceException(message));
            }

            return null;
        }

        private IValidationResult ValidateLanguage(string language)
        {
            if (string.IsNullOrEmpty(language))
            {
                var message = "Received language is null or empty.";
                return new ValidationResult(false, message, new NullReferenceException(message));
            }

            return null;
        }

        private IValidationResult ValidateCorrections(IEnumerable<CorrectionDto> corrections)
        {
            if (corrections == null)
            {
                var message = "Received corrections are null or empty.";
                return new ValidationResult(false, message, new NullReferenceException(message));
            }

            return null;
        }

        #endregion
    }
}
