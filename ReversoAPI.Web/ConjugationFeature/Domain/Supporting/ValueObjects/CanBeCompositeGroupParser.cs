using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReversoAPI.Web.ConjugationFeature.Domain.Supporting.ValueObjects
{
    /// <summary>
    /// Conjugation Parser
    /// 
    /// Some languages in Reverso layout have composite words, namely words are split into two parts.
    /// For example instead of 'бежать' we get 'бе жать'.
    /// 
    /// But some words are simplex
    /// 
    /// That is actual for Russian and Portuguese
    /// </summary>
    public class CanBeCompositeGroupParser : ConjugationParser
    {
        public static Language[] SupportedLanguages { get; } =
        {
            Language.Russian,
            Language.Portuguese
        };

        public CanBeCompositeGroupParser(HtmlDocument html, Language language) : base(html, language)
        {
        }

        public override Dictionary<string, IEnumerable<Conjugation>> Parse()
        {
            if (!SupportedLanguages.Contains(_language)) throw new ArgumentException($"{nameof(CanBeCompositeGroupParser)} does not support '{_language}' language");

            var conjugations = new Dictionary<string, IEnumerable<Conjugation>>();
            var composite = IsComposite();

            var root = composite ?
                _html.DocumentNode.SelectSingleNode("//*[@class='hglhOver']")?.InnerHtml.Trim() ??  // For Non-Infinitive
                _html.DocumentNode.SelectSingleNode("//*[@class='verbtxt']")?.InnerHtml.Trim() ??   // For Infinitive
                throw new ParsingException("Failed to parse root of the word") :
                string.Empty;

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

                    var verbs = _html.DocumentNode.SelectNodes(GetXPathGrid(row, col) + $"//i[{(composite ? "contains(@class, 'verbtxt-term')" : "@class='verbtxt'")}]")
                                                 ?.Where(n => n.ParentNode.ParentNode.GetAttributeValue("class", null) != "transliteration")
                                                 .Select(n => n.InnerHtml.Trim())
                                                 .Distinct()
                                                 .Select(v => new Conjugation(groupName, root + v, _language))
                                                 ?? Enumerable.Empty<Conjugation>();

                    conjugations.Add(groupName, verbs);
                    col++;
                }
            }

            return conjugations;
        }

        private bool IsComposite() => _html.DocumentNode.SelectSingleNode("//*[@class='verbtxt']") != null;
    }
}
