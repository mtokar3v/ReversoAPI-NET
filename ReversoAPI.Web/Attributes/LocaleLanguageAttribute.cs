using ReversoAPI.Web.Values;
using System;

namespace ReversoAPI.Web.Attributes
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
