using ReversoAPI.Web.Attributes;
using ReversoAPI.Web.Values;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ReversoAPI.Web.Extensions
{
    public static class ParseExtensions
    {
        // Revise this approach if it is possible

        private static HashSet<string> AdverbPseudonyms = new HashSet<string>
        {
            "adv.",
            "adverb",
        };

        private static HashSet<string> VerbPseudonyms = new HashSet<string>()
        {
            "v.",
            "verb",
        };

        private static HashSet<string> NounPseudonyms = new HashSet<string>()
        {
            "n.",
            "nn.",
            "nf.",
            "nm.",
            "noun",
            "noun - masculine",
            "noun - neutral"
        };

        private static HashSet<string> AdjectivePseudonyms = new HashSet<string>()
{
            "adj.",
            "adjective",
        };

        public static PartOfSpeech ToPartOfSpeech(this string value)
        {
            if (string.IsNullOrEmpty(value)) return PartOfSpeech.Unknown;

            return value?.ToLower() switch
            {
                var v when AdverbPseudonyms.Contains(v) => PartOfSpeech.Adverb,
                var v when VerbPseudonyms.Contains(v) => PartOfSpeech.Verb,
                var v when NounPseudonyms.Contains(v) => PartOfSpeech.Noun,
                var v when AdjectivePseudonyms.Contains(v) => PartOfSpeech.Adjective,
                _ => PartOfSpeech.Unknown
            };
        }


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
            var attribute = value.GetAttribute<ShortNameAttribute, T>();
            return attribute == null ? null : attribute.Name;
        }

        public static string ToMediumName<T>(this T value) 
        {
            var attribute = value.GetAttribute<MediumNameAttribute, T>();
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
