﻿using System;
using ReversoAPI.Web.Shared.Application.Interfaces;

namespace ReversoAPI.Web.Shared.Application.Validators
{
    public class ValidationResult : IValidationResult
    {
        private readonly Exception _exception;

        public ValidationResult(bool isValid)
        {
            IsValid = isValid;
        }

        public ValidationResult(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }

        public ValidationResult(bool isValid, string message, Exception exception)
        {
            IsValid = isValid;
            Message = message;

            _exception = exception;
        }

        public bool IsValid { get; }
        public string Message { get; }
        public Exception Exception => _exception ?? (!IsValid ? new Exception(Message) : null);
    }
}
