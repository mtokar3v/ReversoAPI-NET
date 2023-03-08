using System;
using System.Linq;
using System.Collections.Generic;
using ReversoAPI.Web.Exceptions;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Values;
using HtmlAgilityPack;
using ReversoAPI.Web.Domain.Core.Context.ValueObjects;
using ReversoAPI.Web.Context.Domain.Core.Context.ValueObjects;
using ReversoAPI.Web.Context.Domain.Core.Enities;
using ReversoAPI.Web.Context.Domain.Core.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.Context.Domain.Supporting.Builders
{
    internal class ContextBuilder
    {
        private readonly HtmlDocument _html;
        private readonly ContextData _response;

        public ContextBuilder(HtmlDocument html)
        {
            _html = html;
            _response = new ContextData();
        }

        public ContextData Build() => _response;

        public ContextBuilder WithInputText()
        {
            try
            {
                _response.Text = _html.DocumentNode
                    .SelectSingleNode("//*[@id='search-input']//input[1]")
                    .GetAttributeValue("value", string.Empty);

                return this;
            }
            catch
            {
                throw new ParsingException("Unable to parse input field.");
            }
        }

        public ContextBuilder WithLanguages()
        {
            try
            {
                _response.Source = _html.DocumentNode
                    .SelectSingleNode("//*[@id='src-selector']//span[@class='option front']")
                    .GetAttributeValue("data-value", string.Empty)
                    .ToLanguageFromShortName();

                if (_response.Source == Language.Unknown) throw new ParsingException();
            }
            catch
            {
                throw new ParsingException("Unable to parse source language.");
            }

            try
            {
                _response.Target = _html.DocumentNode
                    .SelectSingleNode("//*[@id='trg-selector']//span[@class='option front']")
                    .GetAttributeValue("data-value", string.Empty)
                    .ToLanguageFromShortName();

                if (_response.Target == Language.Unknown) throw new ParsingException();
            }
            catch
            {
                throw new ParsingException("Unable to parse target language.");
            }

            return this;
        }

        public ContextBuilder WithExamples()
        {
            var sourceLanguage = _response.Source;
            if (sourceLanguage == Language.Unknown) throw new ArgumentException($"'{_response.Source}' is not setted");

            var targetLanguage = _response.Target;
            if (targetLanguage == Language.Unknown) throw new ArgumentException($"'{_response.Target}' is not setted");

            try
            {
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
            catch
            {
                throw new ParsingException("Unable to parse contexts examples.");
            }
        }

        private string GetLayout(Language language)
        {
            return language == Language.Arabic ? $"rtl {Language.Arabic.ToString().ToLower()}" :
                   language == Language.Hebrew ? "rtl" :
                   "ltr";
        }
    }
}
