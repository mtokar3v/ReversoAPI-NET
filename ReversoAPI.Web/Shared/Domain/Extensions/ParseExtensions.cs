using System.Collections.Generic;
using System.Text.RegularExpressions;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.Shared.Domain.Extensions
{
    public static class ParseExtensions
    {
        // TO DO: Revise this approach if it is possible

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
