using System;

namespace ReversoAPI.Web.Shared.Domain.Exceptions
{
    public class ParsingException : Exception
    {
        public ParsingException() { }
        public ParsingException(string message) : base(message) { }
    }
}
