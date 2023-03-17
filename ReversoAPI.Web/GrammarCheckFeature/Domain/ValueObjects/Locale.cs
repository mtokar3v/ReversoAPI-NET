namespace ReversoAPI
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
