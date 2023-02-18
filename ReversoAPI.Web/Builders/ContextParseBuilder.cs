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
    internal class ContextParseBuilder
    {
        private readonly HtmlDocument _html;
        private readonly ContextData _response;

        public ContextParseBuilder(HtmlDocument html)
        {
            _html = html;
            _response = new ContextData();
        }

        public ContextData Build() => _response;

        public ContextParseBuilder WithInputText()
        {
            _response.Text = _html.DocumentNode
                .SelectSingleNode("//*[@id='search-input']//input[1]")
                .GetAttributeValue("value", string.Empty);

            return this;
        }

        public ContextParseBuilder WithLanguages()
        {
            _response.Source = _html.DocumentNode
                .SelectSingleNode("//*[@id='src-selector']//span[@class='option front']")
                .GetAttributeValue("data-value", string.Empty)
                .ToLanguageFromShortName();

            _response.Target = _html.DocumentNode
                .SelectSingleNode("//*[@id='trg-selector']//span[@class='option front']")
                .GetAttributeValue("data-value", string.Empty)
                .ToLanguageFromShortName();

            return this;
        }

        public ContextParseBuilder WithTranslations()
        {
            var targetLanguage = _response.Target;
            if (targetLanguage == Language.Unknown) throw new ArgumentException($"'{_response.Target}' is not setted");

            _response.Translations = _html.DocumentNode
                            .SelectNodes("//*[@id='translations-content']//span[@class='display-term']")
                            ?.Select(n => new Word(n.InnerHtml.ReplaceSpecSymbols(), targetLanguage, GetPartOfSpeech(n)));

            return this;

            PartOfSpeech GetPartOfSpeech(HtmlNode node) => node.ParentNode
                    ?.ChildNodes
                    ?.FirstOrDefault(n => n.Name == "div")
                    ?.ChildNodes
                    ?.FirstOrDefault(n => n.Name == "span")
                    //maybe should use short names (class name) instead of title, cause title depends of page language
                    ?.GetAttributeValue("title", string.Empty)
                    ?.ToPartOfSpeech()
                    ?? PartOfSpeech.Unknown;
        }

        public ContextParseBuilder WithExamples()
        {
            var sourceLanguage = _response.Source;
            if (sourceLanguage == Language.Unknown) throw new ArgumentException($"'{_response.Source}' is not setted");

            var targetLanguage = _response.Target;
            if (targetLanguage == Language.Unknown) throw new ArgumentException($"'{_response.Target}' is not setted");

            var sourceLayout = GetLayout(sourceLanguage);
            var sourceSentences = _html.DocumentNode
                .SelectNodes($"//*[@id='examples-content']/div[@class='example']/div[@class='src {sourceLayout}']/span")
                .Select(n => n.InnerHtml.RemoveHtmlTags().ReplaceSpecSymbols());

            var targetLayout = GetLayout(targetLanguage);
            var targetSentences = _html.DocumentNode
                .SelectNodes($"//*[@id='examples-content']/div[@class='example']/div[@class='trg {targetLayout}']/span[@class='text'][1]")
                .Select(n => n.InnerHtml.RemoveHtmlTags().ReplaceSpecSymbols());

            if (targetSentences.Count() != sourceSentences.Count())
                throw new ParsingException("Failed to parse an examples");

            var examples = new List<Example>();

            for (var i = 0; i < targetSentences.Count(); i++)
            {
                var sourceSentence = new Sentence(sourceLanguage, sourceSentences.ElementAt(i));
                var targetSentence = new Sentence(targetLanguage, targetSentences.ElementAt(i));
                examples.Add(new Example(sourceSentence, targetSentence));
            }

            _response.Examples = examples;

            return this;
        }

        private string GetLayout(Language language)
        {
            return language == Language.Arabic ? $"rtl {Language.Arabic.ToString().ToLower()}" :
                   language == Language.Hebrew ? "rtl" :
                   "ltr";
        }
    }
}
