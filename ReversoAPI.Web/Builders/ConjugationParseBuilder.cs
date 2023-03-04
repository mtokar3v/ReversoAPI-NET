using System;
using System.Linq;
using System.Collections.Generic;
using ReversoAPI.Web.Values;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Exceptions;
using HtmlAgilityPack;

namespace ReversoAPI.Web.Builders
{
    internal class ConjugationParseBuilder
    {
        private static Language[] _canBeCompositeLanguages = 
        {
            Language.Russian,
            Language.Portuguese
        };

        private readonly HtmlDocument _html;
        private readonly ConjugationData _response;

        public ConjugationParseBuilder(HtmlDocument html)
        {
            _html = html;
            _response = new ConjugationData();
        }

        public ConjugationData Build() => _response;

        public ConjugationParseBuilder WithInputText()
        {
            try
            {
                _response.Text = _html.DocumentNode
                    .SelectSingleNode("//*[@id='txtVerb']")
                    .GetAttributeValue("value", string.Empty);

                return this;
            }
            catch
            {
                throw new ParsingException("Unable to parse input field.");
            }
        }

        public ConjugationParseBuilder WithLanguage()
        {
            try
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
            catch
            {
                throw new ParsingException("Unable to parse language.");
            }
        }

        public ConjugationParseBuilder WithConjugations()
        {
            var language = _response.Language;
            if (language == Language.Unknown) throw new ArgumentException($"'{_response.Language}' is not setted");

            try
            {
                var conjugations = new Dictionary<string, IEnumerable<Conjugation>>();

                var composite = IsComposite(language);
                var root = composite ?
                    _html.DocumentNode.SelectSingleNode("//*[@class='hglhOver']")?.InnerHtml.Trim() ??  // Non-infinitive
                    _html.DocumentNode.SelectSingleNode("//*[@class='verbtxt']")?.InnerHtml.Trim() ??   // Infinitive
                    throw new ParsingException("Failed to parse root of the word") :
                    string.Empty;

                
                var rowsCount = _html.DocumentNode.SelectNodes("//div[@class='word-wrap-row']").Count();

                for (var row = 1; row <= rowsCount; row++)
                {
                    var col = 1;
                    while (true)
                    {
                        var gridXPath = GetXPathGrid(row, col);

                        var blueBoxWrap = _html.DocumentNode.SelectSingleNode(gridXPath);
                        if (blueBoxWrap == null) break;

                        var groupName = blueBoxWrap.GetAttributeValue("mobile-title", null).Trim();

                        var verbs = _html.DocumentNode.SelectNodes(gridXPath + $"//i[{GetXPathVerbSelector(composite, language)}]")
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
            catch
            {
                throw new ParsingException("Unable to parse conjugations.");
            }
        }

        /// <summary>
        /// The word splits into two parts in UI
        /// </summary>
        private bool IsComposite(Language language) 
            => _canBeCompositeLanguages.Contains(language) && _html.DocumentNode.SelectSingleNode("//*[@class='verbtxt']") != null;

        private string GetXPathVerbSelector(bool isComposite, Language language) 
            => (isComposite, language) switch
            {
                (false, _) => "@class='verbtxt'",
                (_, Language.Portuguese) => string.Join(" or ", (new[] { "verbtxt-term", "verbtxt-term-irr" }).Select(c => $"@class='{c}'")),
                (_, Language.Russian) => "@class='verbtxt-term'",
                _ => throw new NotImplementedException()
            };

        private string GetXPathGrid(int row, int col) 
            => $"//div[@class='word-wrap-row'][{row}]/div[@class='wrap-three-col'][{col}]/div[@class='blue-box-wrap' or @class='blue-box-wrap alt-tense']";
    }
}
