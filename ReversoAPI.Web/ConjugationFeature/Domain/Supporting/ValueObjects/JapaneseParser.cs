using System;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace ReversoAPI.Web.ConjugationFeature.Domain.Supporting.ValueObjects
{
    public class JapaneseParser : ConjugationParser
    {
        public static Language[] SupportedLanguages { get; } =
        {
            Language.Japanese
        };

        public JapaneseParser(HtmlDocument html, Language language) : base(html, language)
        {
        }

        public override Dictionary<string, IEnumerable<Conjugation>> Parse()
        {
            if (!SupportedLanguages.Contains(_language)) throw new ArgumentException($"{nameof(JapaneseParser)} does not support '{_language}' language");

            var conjugations = new Dictionary<string, IEnumerable<Conjugation>>();

            var rowsCount = _html.DocumentNode.SelectNodes(XPathWrapper).Count();

            for (var row = 1; row <= rowsCount; row++)
            {
                var col = 1;
                while (true)
                {
                    var groupName = _html.DocumentNode
                            .SelectNodes($"{XPathWrapper}[{row}]/div[@class='wrap-three-col'][{col}]//div[contains(@class, 'blue-box-wrap tense-pos')]")
                            ?.ElementAtOrDefault(0)
                            ?.GetAttributeValue("mobile-title", null)
                            .Trim();

                    if (string.IsNullOrEmpty(groupName))
                        break;

                    var verbs = _html.DocumentNode.SelectNodes(GetXPathGrid(row, col) + $"//i[@class='verbtxt']//span[@class='ruby']")
                                                 .Select(n => n.GetAttributeValue("value", null))
                                                 .Distinct()
                                                 .Select(v => new Conjugation(groupName, v, _language))
                                                 ?? Enumerable.Empty<Conjugation>();

                    conjugations.Add(groupName, verbs);
                    col++;
                }
            }

            return conjugations;
        }
    }
}
