using System;
using System.Text;

namespace ReversoAPI.Web.Shared.Application.Extensions
{
    public static class EncodeExtensions
    {
        static public string EncodeTo64(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }
    }
}
