using System;

namespace ReversoAPI.Web.Exceptions
{
    public class ParsingException : Exception
    {
        public ParsingException() { }
        public ParsingException(string message) : base(message) { }
    }
}
