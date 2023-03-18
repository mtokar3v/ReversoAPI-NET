using System;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace ReversoAPI.Web.ConjugationFeature.Domain.Supporting.ValueObjects
{
    /// <summary>
    /// Conjugation Parser
    /// Most of all languages have same layout
    /// </summary>
    public class CommonGroupParser : ConjugationParser
    {
        public static Language[] SupportedLanguages { get; } =
        {
            Language.English, Language.French,
            Language.Spanish, Language.German,
            Language.Italian
        };

        public CommonGroupParser(HtmlDocument html, Language language) : base(html, language)
        {
        }

        public override Dictionary<string, IEnumerable<Conjugation>> Parse()
        {
            if (!SupportedLanguages.Contains(_language)) throw new ArgumentException($"{nameof(CommonGroupParser)} does not support '{_language}' language");

            var conjugations = new Dictionary<string, IEnumerable<Conjugation>>();

            var rowsCount = _html.DocumentNode.SelectNodes(XPathWrapper).Count();

            for (var row = 1; row <= rowsCount; row++)
            {
                var col = 1;
                while (true)
                {
                    var groupName = _html.DocumentNode
                        .SelectSingleNode(GetXPathGrid(row, col))
                        ?.GetAttributeValue("mobile-title", null)
                        .Trim();

                    if (string.IsNullOrEmpty(groupName))
                        break;

                    var verbs = _html.DocumentNode.SelectNodes(GetXPathGrid(row, col) + $"//i[@class='verbtxt']")
                                                 ?.Where(n => n.ParentNode.ParentNode.GetAttributeValue("class", null) != "transliteration")
                                                 .Select(n => n.InnerHtml.Trim())
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
