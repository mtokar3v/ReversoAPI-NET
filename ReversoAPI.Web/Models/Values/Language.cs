using ReversoAPI.Web.Attributes;

namespace ReversoAPI.Web.Models.Values
{
    public enum Language
    {
        Unknown,

        [ShortName("en")]
        English,

        [ShortName("ru")]
        Russian,

        [ShortName("ar")]
        Arabic,

        [ShortName("he")]
        Hebrew,
    }
}
