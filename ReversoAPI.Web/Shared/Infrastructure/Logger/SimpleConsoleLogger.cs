using System;
using ReversoAPI.Web;

namespace ReversoAPI
{
    public class SimpleConsoleLogger : ILogger
    {
        private string Now => DateTime.UtcNow.ToString("U");  
        public void Debug(string message) => Console.WriteLine(Now + Environment.NewLine + $"DEBUG: {message}");
        public void Info(string message) => Console.WriteLine(Now + Environment.NewLine + $"INFO: {message}");
        public void Warning(string message) => Console.WriteLine(Now + Environment.NewLine + $"WARNING: {message}");
        public void Error(string message) => Console.WriteLine(Now + Environment.NewLine + $"ERROR: {message}");
        public void Fatal(string message) => Console.WriteLine(Now + Environment.NewLine + $"FATAL: {message}");
    }
}
