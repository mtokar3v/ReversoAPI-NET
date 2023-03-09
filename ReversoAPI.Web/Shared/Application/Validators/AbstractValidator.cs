using System;
using System.Collections.Generic;
using ReversoAPI.Web.Shared.Application.Interfaces;

namespace ReversoAPI.Web.Shared.Application.Validators
{
    public abstract class AbstractValidator
    {
        public IValidationResult Validate()
        {
            var validators = GetValidators();

            foreach (var validator in validators)
            {
                var result = validator();
                if (result != null) return result;
            }

            return new ValidationResult(isValid: true);
        }

        protected abstract IEnumerable<Func<IValidationResult>> GetValidators();
    }
}