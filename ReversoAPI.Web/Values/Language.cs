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
        [MediumName("rus")]
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

        German,

        Japanese,

        Dutch,

        Polish,

        Portuguese,

        Romanian,

        Turkish,

        Chinese,
    }
}
