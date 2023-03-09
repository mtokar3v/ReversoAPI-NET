using System;

namespace ReversoAPI.Web.Shared.Application.Interfaces
{
    public interface IValidationResult
    {
        bool IsValid { get; }
        string Message { get; }
        Exception Exception { get; }
    }
}