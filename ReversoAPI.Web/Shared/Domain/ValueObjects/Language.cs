using ReversoAPI.Web.Shared.Domain.Attributes;

namespace ReversoAPI.Web.Shared.Domain.ValueObjects
{
    public enum Language
    {
        Unknown,

        [ShortName("ar")]
        [MediumName("ara")]
        Arabic,

        [ShortName("de")]
        [MediumName("ger")]
        German,

        [ShortName("en")]
        [MediumName("eng")]
        English,

        [ShortName("es")]
        [MediumName("spa")]
        Spanish,

        [ShortName("fr")]
        [MediumName("fra")]
        French,

        [ShortName("he")]
        [MediumName("heb")]
        Hebrew,

        [ShortName("it")]
        [MediumName("ita")]
        Italian,

        [ShortName("ja")]
        [MediumName("jpn")]
        Japanese,

        [ShortName("ko")]
        [MediumName("kor")]
        Korean,

        [ShortName("cs")]
        [MediumName("cze")]
        Czech,

        [ShortName("da")]
        [MediumName("dan")]
        Danish,

        [ShortName("el")]
        [MediumName("gre")]
        Greek,

        [ShortName("hi")]
        [MediumName("hin")]
        Hindi,

        [ShortName("nl")]
        [MediumName("dut")]
        Dutch,

        [ShortName("pl")]
        [MediumName("pol")]
        Polish,

        [ShortName("pt")]
        [MediumName("por")]
        Portuguese,

        [ShortName("ro")]
        [MediumName("rum")]
        Romanian,

        [ShortName("ru")]
        [MediumName("rus")]
        Russian,

        [ShortName("sv")]
        [MediumName("swe")]
        Swedish,

        [ShortName("tr")]
        [MediumName("tur")]
        Turkish,

        [ShortName("uk")]
        [MediumName("ukr")]
        Ukrainian,

        [ShortName("zh")]
        [MediumName("chi")]
        Chinese,

        [ShortName("hu")]
        [MediumName("hun")]
        Hungarian,

        [ShortName("fa")]
        [MediumName("per")]
        Persian,

        [ShortName("sk")]
        [MediumName("slo")]
        Slovak,

        [ShortName("th")]
        [MediumName("tha")]
        Thai,
    }
}
