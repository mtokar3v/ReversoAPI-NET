using HtmlAgilityPack;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Exceptions;
using ReversoAPI.Web.Values;
using ReversoAPI.Web.Values.ConjugationObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReversoAPI.Web.Builders
{
    internal class ConjugationParseBuilder
    {
        private static Language[] _compositeLanguages =
{
            Language.Russian,
        };

        private readonly HtmlDocument _html;
        private readonly ConjugationData _response;

        public ConjugationParseBuilder(HtmlDocument html)
        {
            _html = html;
            _response = new ConjugationData();
        }

        public ConjugationData Build() => _response;


        public ConjugationParseBuilder WithLanguage()
        {
            var title = _html.DocumentNode.SelectSingleNode("//title").InnerHtml;

            foreach (Language l in Enum.GetValues(typeof(Language)))
            {
                if (title.Contains(l.ToString()))
                {
                    _response.Language = l;
                    return this;
                }
            }

            _response.Language = Language.Unknown;
            return this;
        }

        public ConjugationParseBuilder WithConjugations()
        {
            var language = _response.Language;
            if (language == Language.Unknown) throw new ArgumentException($"'{_response.Language}' is not setted");

            var conjugations = new Dictionary<string, IEnumerable<Conjugation>>();

            var composite = IsComposite(language);
            var root = composite
                ? _html.DocumentNode.SelectSingleNode("//*[@class='verbtxt']")?.InnerHtml.Trim() ?? throw new ParsingException("Failed to parse root of the word")
                : string.Empty;

            var rowsCount = _html.DocumentNode.SelectNodes("//div[@class='word-wrap-row']").Count();

            for (var row = 1; row <= rowsCount; row++)
            {
                var col = 1;
                while (true)
                {
                    var gridXPath = $"//div[@class='word-wrap-row'][{row}]/div[@class='wrap-three-col'][{col}]/div[@class='blue-box-wrap' or @class='blue-box-wrap alt-tense']";

                    var blueBoxWrap = _html.DocumentNode.SelectSingleNode(gridXPath);
                    if (blueBoxWrap == null) break;

                    var groupName = blueBoxWrap.GetAttributeValue("mobile-title", null).Trim();

                    var verbs = _html.DocumentNode.SelectNodes(gridXPath + $"//i[@class='{(composite ? "verbtxt-term" : "verbtxt")}']")
                                                 ?.Where(n => n.ParentNode.ParentNode.GetAttributeValue("class", null) != "transliteration")
                                                 .Select(n => n.InnerHtml.Trim())
                                                 .Distinct()
                                                 .Select(v => new Conjugation(groupName, root + v, language))
                                                 ?? new List<Conjugation>();

                    conjugations.Add(groupName, verbs);
                    col++;
                }
            }

            _response.Conjugations = conjugations;
            return this;
        }

        /// <summary>
        /// The word splits into two parts in UI
        /// </summary>
        private bool IsComposite(Language language) => _compositeLanguages.Contains(language);
    }
}
