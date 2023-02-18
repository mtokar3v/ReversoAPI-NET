using ReversoAPI.Web.Attributes;

namespace ReversoAPI.Web.Values
{
    public enum Language
    {
        Unknown,

        [ShortName("en")]
        [MediumName("eng")]
        English,

        [ShortName("ru")]
        Russian,

        [ShortName("ar")]
        Arabic,

        [ShortName("he")]
        Hebrew,

        [MediumName("fra")]
        French,

        [MediumName("spa")]
        Spanish,

        [MediumName("ita")]
        Italian,
    }
}
