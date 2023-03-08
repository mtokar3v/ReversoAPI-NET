using ReversoAPI.Web.Attributes;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.GrammarCheck.Domain.ValueObjects
{
    public enum Locale
    {
        None,

        [LocaleLanguage(Language.English)]
        UK,

        [LocaleLanguage(Language.English)]
        US,

        // Other languages still unsupported
    }
}
