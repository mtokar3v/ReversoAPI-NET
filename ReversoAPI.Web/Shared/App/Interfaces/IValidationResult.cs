using System;

namespace ReversoAPI.Web.Shared.App.Interfaces
{
    internal interface IValidationResult
    {
        bool IsValid { get; }
        string Message { get; }
        Exception Exception { get; }
    }
}