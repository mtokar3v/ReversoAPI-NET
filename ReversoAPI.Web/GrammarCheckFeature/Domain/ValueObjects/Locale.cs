using ReversoAPI.Web.Shared.Domain.Attributes;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.GrammarCheckFeature.Domain.ValueObjects
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
