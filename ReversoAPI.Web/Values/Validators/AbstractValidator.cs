using System;
using System.Collections.Generic;

namespace ReversoAPI.Web.Values.Validators
{
    internal abstract class AbstractValidator
    {
        public IValidationResult Validate()
        {
            var validators =  GetValidators();

            foreach (var validator in validators)
            {
                var result = validator();
                if (result != null) return result;
            }

            return new ValidationResult(isValid: true);
        }

        public abstract IEnumerable<Func<IValidationResult>> GetValidators();
    }
}