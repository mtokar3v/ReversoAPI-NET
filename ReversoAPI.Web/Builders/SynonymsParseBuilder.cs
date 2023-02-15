using HtmlAgilityPack;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Models.Entities;
using ReversoAPI.Web.Models.Responses;
using ReversoAPI.Web.Models.Values;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReversoAPI.Web.Builders
{
    public class SynonymsParseBuilder
    {
        private readonly HtmlDocument _html;
        private readonly SynonymsResponse _response;

        public SynonymsParseBuilder(HtmlDocument html)
        {
            _html = html;
            _response = new SynonymsResponse();
        }

        public SynonymsResponse Build() => _response;

        public SynonymsParseBuilder WithInputText()
        {
            _response.Text = _html.DocumentNode.SelectSingleNode("//div[@class='search-title']").InnerHtml;
            return this;
        }

        public SynonymsParseBuilder WithLanguage()
        {
            _response.Source = _html.DocumentNode.SelectSingleNode("//*[@id='trg-selector']//span[@class='lang-trg'][1]").InnerHtml.ToLanguage();
            return this;
        }

        public SynonymsParseBuilder WithSynonyms()
        {
            var language = _response.Source;
            if (language == Language.Unknown) throw new ArgumentException($"'{_response.Source}' is not setted");

            var partsOfSpeech = _html.DocumentNode
                .SelectNodes("//*[@class='wrap-hold-prop']//div/h2")
                .Select(n => n.InnerHtml.ToPartOfSpeech());

            var synonyms = new List<Word>();

            foreach (var partOfSpeech in partsOfSpeech.Select((value, index) => new { index, value }))
            {
                var synonymTexts = _html.DocumentNode
                    .SelectNodes($"//div[@class='wrap-hold-prop'][{partOfSpeech.index + 1}]//a[@class='synonym  relevant' or @class='synonym ']")
                    .Select(n => n.InnerHtml.ReplaceSpecSymbols());

                synonyms.AddRange(synonymTexts.Select(s => new Word(s, language, partOfSpeech.value)));
            }

            _response.Synonyms = synonyms;

            return this;
        }
    }
}
