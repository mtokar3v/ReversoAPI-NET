using ReversoAPI.Web.Shared.Domain.Extensions;

namespace ReversoAPI.Web.TranslationFeature.Application.DTOs
{
    public class TranslationRequest
    {
        public TranslationRequest(string text, Language source, Language target)
        {
            Input = text;
            From = source.ToMediumName();
            To = target.ToMediumName();

            Format = "Text";
            Options = new OptionsDto
            {
                ContextResults = true,
                LanguageDetection = true,
                Origin = "translation.web",
                SentenceSplitter = true,
            };
        }

        public string Format { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Input { get; set; }
        public OptionsDto Options { get; set; }
    }

    public class OptionsDto
    {
        public bool ContextResults { get; set; }
        public bool LanguageDetection { get; set; }
        public string Origin { get; set; }
        public bool SentenceSplitter { get; set; }
    }
}
