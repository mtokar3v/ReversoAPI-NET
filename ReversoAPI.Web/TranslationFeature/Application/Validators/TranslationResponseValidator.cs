using System;
using System.Linq;
using System.Collections.Generic;
using ReversoAPI.Web.TranslationFeature.Application.DTOs;
using ReversoAPI.Web.Shared.Application.Interfaces;
using ReversoAPI.Web.Shared.Application.Validators;

namespace ReversoAPI.Web.TranslationFeature.Application.Validators
{
    public class TranslationResponseValidator : AbstractValidator
    {
        public TranslationResponseValidator(TranslationResponse response)
        {
            Response = response;
        }

        public TranslationResponse Response { get; }

        protected override IEnumerable<Func<IValidationResult>> GetValidators()
        {
            Func<IValidationResult>[] validators =
            {
                () => ValidateResponse(Response),
                () => ValidateFrom(Response.From),
                () => ValidateTo(Response.To),
                () => ValidateInput(Response.Input),
                () => ValidateTranslations(Response.Translation),
            };

            return validators;
        }

        #region Private Methods

        private IValidationResult ValidateResponse(TranslationResponse response)
        {
            if (response == null)
            {
                var message = "Reverso translation response in empty.";
                return new ValidationResult(false, message);
            }

            return null;
        }

        private IValidationResult ValidateFrom(string from)
        {
            if (string.IsNullOrEmpty(from))
            {
                var message = $"Received 'From' is null or empty.";
                return new ValidationResult(false, message);
            }

            return null;
        }

        private IValidationResult ValidateTo(string to)
        {
            if (string.IsNullOrEmpty(to))
            {
                var message = $"Received 'To' is null or empty.";
                return new ValidationResult(false, message);
            }

            return null;
        }

        private IValidationResult ValidateInput(IEnumerable<string> input)
        {
            if (input == null || !input.Any())
            {
                var message = $"Received input is null or empty.";
                return new ValidationResult(false, message);
            }

            return null;
        }

        private IValidationResult ValidateTranslations(IEnumerable<string> translations)
        {
            if (translations == null || !translations.Any())
            {
                var message = $"Received translations is null or empty.";
                return new ValidationResult(false, message);
            }

            return null;
        }

        #endregion
    }
}
