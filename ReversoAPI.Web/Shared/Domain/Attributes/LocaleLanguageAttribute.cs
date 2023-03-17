using System;

namespace ReversoAPI
{
    public class LocaleLanguageAttribute : Attribute
    {
        public Language Language { get; set; }
        public LocaleLanguageAttribute(Language language)
        {
            Language = language;
        }
    }
}
