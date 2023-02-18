using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Values;

namespace ReversoAPI.Web.DTOs.SpellingResponseData
{
    public class SpellingRequest
    {
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
            Locale = locale == Values.Locale.None ? string.Empty : locale.ToString();
            Origin = "interactive";
            OriginalText = string.Empty;
            SpellingFeedbackOptions = new SpellingFeedbackOptions
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
        public SpellingFeedbackOptions SpellingFeedbackOptions { get; set; }
        public string Text { get; set; }
    }

    public class SpellingFeedbackOptions
    {
        public bool InsertFeedback { get; set; }
        public bool UserLoggedOn { get; set; }
    }
}
