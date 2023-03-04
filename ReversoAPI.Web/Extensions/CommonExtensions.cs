using ReversoAPI.Web.Attributes;
using ReversoAPI.Web.Values;
using System;

namespace ReversoAPI.Web.Extensions
{
    public static class CommonExtensions
    {
        public static Language ToLanguageFromMediumName(this string value)
        {
            if (string.IsNullOrEmpty(value)) return Language.Unknown;

            foreach (Language l in Enum.GetValues(typeof(Language)))
            {
                if (l.ToMediumName() == value)
                    return l;
            }

            return Language.Unknown;
        }

        public static Language ToLanguageFromShortName(this string value)
        {
            if (string.IsNullOrEmpty(value)) return Language.Unknown;

            foreach (Language l in Enum.GetValues(typeof(Language)))
            {
                if (l.ToShortName() == value)
                    return l;
            }

            return Language.Unknown;
        }

        public static Language ToLanguage(this string value)
        {
            if (string.IsNullOrEmpty(value)) return Language.Unknown;

            return Enum.TryParse(value, out Language language) ? language : Language.Unknown;
        }

        public static string ToShortName<T>(this T value)
        {
            var attribute = value.GetAttribute<ShortNameAttribute, T>();
            return attribute == null ? null : attribute.Name;
        }

        public static string ToMediumName<T>(this T value)
        {
            var attribute = value.GetAttribute<MediumNameAttribute, T>();
            return attribute == null ? null : attribute.Name;
        }

        public static Language GetLanguage(this Locale locale)
        {
            var attribute = locale.GetAttribute<LocaleLanguageAttribute, Locale>();
            return attribute == null ? Language.Unknown : attribute.Language;
        }
    }
}
