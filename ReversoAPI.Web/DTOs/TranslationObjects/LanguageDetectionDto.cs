namespace ReversoAPI.Web.DTOs.TranslationObjects
{
    public class LanguageDetectionDto
    {
        public string DetectedLanguage { get; set; }
        public bool IsDirectionChanged { get; set; }
        public string OriginalDirection { get; set; }
        public int OriginalDirectionContextMatches { get; set; }
        public int ChangedDirectionContextMatches { get; set; }
        public int TimeTaken { get; set; }
    }
}
