using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;
using ReversoAPI.Web.ConjugationFeature.Domain.Supporting.Factories;

namespace ReversoAPI.Web.ConjugationFeature.Domain.Supporting.Builders
{
    public class ConjugationParseBuilder
    {
        private static Language[] _canBeCompositeLanguages =
        {
            Language.Russian,
            Language.Portuguese
        };

        private readonly HtmlDocument _html;
        private readonly ConjugationData _response;

        public ConjugationParseBuilder(Stream htmlStream)
        {
            _html = new HtmlDocument();
            _html.Load(htmlStream);
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
                var parser = ConjugationParserFactory.Create(_html, language);
                _response.Conjugations = parser.Parse();
            }
            catch
            {
                throw new ParsingException("Unable to parse conjugations.");
            }

            return this;
        }
    }
}
