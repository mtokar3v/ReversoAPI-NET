using HtmlAgilityPack;
using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Exceptions;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Values;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReversoAPI.Web.Builders
{
    internal class SynonymsParseBuilder
    {
        private readonly HtmlDocument _html;
        private readonly SynonymsData _response;

        public SynonymsParseBuilder(HtmlDocument html)
        {
            _html = html;
            _response = new SynonymsData();
        }

        public SynonymsData Build() => _response;

        public SynonymsParseBuilder WithInputText()
        {
            try
            {
                _response.Text = _html.DocumentNode.SelectSingleNode("//div[@class='search-title']").InnerHtml;
                return this;
            }
            catch
            {
                throw new ParsingException("Unable to parse input field.");
            }
        }

        public SynonymsParseBuilder WithLanguage()
        {
            try
            {
                _response.Source = _html.DocumentNode.SelectSingleNode("//*[@id='trg-selector']//span[@class='lang-trg'][1]").InnerHtml.ToLanguage();
                return this;
            }
            catch
            {
                throw new ParsingException("Unable to parse source language.");
            }
        }

        public SynonymsParseBuilder WithSynonyms()
        {
            var language = _response.Source;
            if (language == Language.Unknown) throw new ArgumentException($"'{_response.Source}' is not setted");

            try
            {
                var partsOfSpeech = _html.DocumentNode
                    .SelectNodes("//*[@class='wrap-hold-prop']//div/h2")
                    .Select(n => n.InnerHtml.ToPartOfSpeech());

                var synonyms = new List<Word>();

                foreach (var partOfSpeech in partsOfSpeech.Select((v, i) => new { Index = i, Value = v }))
                {
                    var synonymTexts = _html.DocumentNode
                        .SelectNodes($"//div[@class='wrap-hold-prop'][{partOfSpeech.Index + 1}]//a[@class='synonym  relevant' or @class='synonym ']")
                        .Select(n => n.InnerHtml.ReplaceSpecSymbols());

                    synonyms.AddRange(synonymTexts.Select(s => new Word(s, language, partOfSpeech.Value)));
                }

                _response.Synonyms = synonyms;

                return this;
            }
            catch
            {
                throw new ParsingException("Unable to parse synonyms.");
            }
        }
    }
}
