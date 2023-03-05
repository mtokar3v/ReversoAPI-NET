using System;

namespace ReversoAPI.Web.Values.Validators
{
    internal interface IValidationResult
    {
        bool IsValid { get; }
        string Message { get; }
        Exception Exception { get; }
    }
}