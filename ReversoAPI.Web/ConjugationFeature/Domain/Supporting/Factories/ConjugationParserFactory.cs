using System;
using System.Linq;
using HtmlAgilityPack;
using ReversoAPI.Web.ConjugationFeature.Domain.Supporting.ValueObjects;

namespace ReversoAPI.Web.ConjugationFeature.Domain.Supporting.Factories
{
    public static class ConjugationParserFactory
    {
        public static ConjugationParser Create(HtmlDocument html, Language language)
        {
            return language switch
            {
                var l when CommonGroupParser.SupportedLanguages.Contains(l)         => new CommonGroupParser(html, language),
                var l when CanBeCompositeGroupParser.SupportedLanguages.Contains(l) => new CanBeCompositeGroupParser(html, language),
                var l when JudeoArabicGroupParse.SupportedLanguages.Contains(l)     => new JudeoArabicGroupParse(html, language),
                var l when JapaneseParser.SupportedLanguages.Contains(l)            => new JapaneseParser(html, language),
                _ => throw new ArgumentOutOfRangeException(nameof(language))
            };
        }
    }
}
