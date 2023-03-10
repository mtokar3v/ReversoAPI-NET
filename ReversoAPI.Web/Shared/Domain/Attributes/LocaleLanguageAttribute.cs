using System;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.Shared.Domain.Attributes
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
