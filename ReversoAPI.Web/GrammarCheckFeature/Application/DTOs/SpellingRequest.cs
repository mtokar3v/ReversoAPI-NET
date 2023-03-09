using ReversoAPI.Web.GrammarCheckFeature.Domain.ValueObjects;
using ReversoAPI.Web.Shared.Domain.Extensions;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.GrammarCheckFeature.Application.DTOs
{
    public class SpellingRequest
    {
        public SpellingRequest()
        {
        }

        public SpellingRequest(string text, Language language, Locale locale)
        {
            Text = text;
            Language = language.ToMediumName();

            IsUserPremium = false;
            AutoReplace = true;
            EnglishDialect = "indifferent";
            GetCorrectionDetails = true;
            InterfaceLanguage = "eu";
            IsHtml = false;
            Locale = locale == Domain.ValueObjects.Locale.None ? string.Empty : locale.ToString();
            Origin = "interactive";
            OriginalText = string.Empty;
            SpellingFeedbackOptions = new SpellingFeedbackOptionsDto
            {
                InsertFeedback = true,
                UserLoggedOn = false
            };
        }

        public bool IsUserPremium { get; set; }
        public bool AutoReplace { get; set; }
        public string EnglishDialect { get; set; }
        public bool GetCorrectionDetails { get; set; }
        public string InterfaceLanguage { get; set; }
        public bool IsHtml { get; set; }
        public string Language { get; set; }
        public string Locale { get; set; }
        public string Origin { get; set; }
        public string OriginalText { get; set; }
        public SpellingFeedbackOptionsDto SpellingFeedbackOptions { get; set; }
        public string Text { get; set; }
    }

    public class SpellingFeedbackOptionsDto
    {
        public bool InsertFeedback { get; set; }
        public bool UserLoggedOn { get; set; }
    }
}
