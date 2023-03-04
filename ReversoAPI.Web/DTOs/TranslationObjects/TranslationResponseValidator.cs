using System;
using System.Linq;
using System.Text;

namespace ReversoAPI.Web.DTOs.TranslationObjects
{
    internal class TranslationResponseValidator
    {
        public static (bool IsValid, string Message) IsValid(TranslationResponse response)
        {
            if (response == null) return (false, "Reverso translation response in empty.");

            var messageBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(response.From)) messageBuilder.AppendLine($"{nameof(response.From)} is null or empty.");
            if (string.IsNullOrEmpty(response.To)) messageBuilder.AppendLine($"{nameof(response.To)} is null or empty.");
            if (response.Input == null || response.Input.Count() == 0) messageBuilder.AppendLine($"{nameof(response.Input)} is null or empty.");
            if (response.Translation == null || response.Translation.Count() == 0) messageBuilder.AppendLine($"{nameof(response.Translation)} is null or empty.");

            var message = messageBuilder.ToString();
            return string.IsNullOrEmpty(message)
                   ? (true, null)
                   : (false, "Response validation is failed: " + Environment.NewLine + message);
        }
    }
}
