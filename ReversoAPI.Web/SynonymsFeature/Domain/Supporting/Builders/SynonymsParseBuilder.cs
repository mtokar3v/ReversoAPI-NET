using System;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;
using ReversoAPI.Web.SynonymsFeature.Domain.Core.Entities;
using ReversoAPI.Web.SynonymsFeature.Domain.Core.Interfaces.Entities;
using ReversoAPI.Web.SynonymsFeature.Domain.Core.Interfaces.ValueObjects;
using ReversoAPI.Web.SynonymsFeature.Domain.Core.ValueObjects;
using ReversoAPI.Web.Shared.Domain.Exceptions;
using ReversoAPI.Web.Shared.Domain.Extensions;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.SynonymsFeature.Domain.Supporting.Builders
{
    public class SynonymsParseBuilder
    {
        private readonly HtmlDocument _html;
        private readonly ISynonymsData _response;

        public SynonymsParseBuilder(HtmlDocument html)
        {
            _html = html;
            _response = new SynonymsData();
        }

        public ISynonymsData Build() => _response;

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
                _response.Language = _html.DocumentNode.SelectSingleNode("//*[@id='trg-selector']//span[@class='lang-trg'][1]").InnerHtml.ToLanguage();
                return this;
            }
            catch
            {
                throw new ParsingException("Unable to parse source language.");
            }
        }

        public SynonymsParseBuilder WithSynonyms()
        {
            var language = _response.Language;
            if (language == Language.Unknown) throw new ArgumentException($"'{_response.Language}' is not setted");

            try
            {
                var partsOfSpeech = _html.DocumentNode
                    .SelectNodes("//*[@class='wrap-hold-prop']//div/h2")
                    .Select(n => n.InnerHtml.ToPartOfSpeech());

                var synonyms = new List<ISynonim>();

                foreach (var partOfSpeech in partsOfSpeech.Select((v, i) => new { Index = i, Value = v }))
                {
                    var synonymTexts = _html.DocumentNode
                        .SelectNodes($"//div[@class='wrap-hold-prop'][{partOfSpeech.Index + 1}]//a[contains(@class, 'synonym')]")
                        .Select(n => n.InnerHtml.ReplaceSpecSymbols());

                    synonyms.AddRange(synonymTexts.Select(s => new Synonim(s, language, partOfSpeech.Value)));
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
