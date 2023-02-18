using ReversoAPI.Web.Attributes;
using ReversoAPI.Web.Models.Values;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReversoAPI.Web.Extensions
{
    public static class ParseExtensions
    {
        public static PartOfSpeech ToPartOfSpeech(this string value)
        {
            if (string.IsNullOrEmpty(value)) return PartOfSpeech.Unknown;

            return value switch
            {
                var v when v.Contains(PartOfSpeech.Adverb.ToString(), StringComparison.InvariantCultureIgnoreCase) => PartOfSpeech.Adverb,
                var v when v.Contains(PartOfSpeech.Verb.ToString(), StringComparison.InvariantCultureIgnoreCase) => PartOfSpeech.Verb,
                var v when v.Contains(PartOfSpeech.Noun.ToString(), StringComparison.InvariantCultureIgnoreCase) => PartOfSpeech.Noun,
                var v when v.Contains(PartOfSpeech.Adjective.ToString(), StringComparison.InvariantCultureIgnoreCase) => PartOfSpeech.Adjective,
                _ => PartOfSpeech.Unknown
            };
        }

        public static Language ToLanguageFromShortName(this string value)
        {
            if (string.IsNullOrEmpty(value)) return Language.Unknown;

            foreach(Language l in Enum.GetValues(typeof(Language)))
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
            ShortNameAttribute attribute = value.GetType()
               .GetField(value.ToString())
               .GetCustomAttributes(typeof(ShortNameAttribute), false)
               .SingleOrDefault() as ShortNameAttribute;

            return attribute == null ? null : attribute.Name;
        }

        public static string RemoveHtmlTags(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return Regex.Replace(value, "<.*?>", string.Empty).Trim();
        }

        public static string ReplaceSpecSymbols(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Replace("\t", "").Replace('\n', ' ').Replace("\r", "");
        }
    }
}
