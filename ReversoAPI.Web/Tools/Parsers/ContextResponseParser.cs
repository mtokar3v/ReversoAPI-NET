using HtmlAgilityPack;
using ReversoAPI.Web.Exceptions;
using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Models.Entities;
using ReversoAPI.Web.Models.Responses;
using ReversoAPI.Web.Models.Values;
using System.Collections.Generic;
using System.Linq;

namespace ReversoAPI.Web.Tools.Parsers
{
    public class ContextResponseParser : BaseResponseParser<ContextResponse>
    {
        public ContextResponseParser(string html) : base(html) { }

        public override ContextResponse Invoke()
        {
            var input = GetInputFromHtml();
            (Language source, Language target) = GetLanguagesFromHtml();
            var translations = GetTranslationsFromHtml(target);
            var examples = GetExamplesFromHtml(source, target);

            return new ContextResponse
            {
                Text = input,
                Source = source,
                Target= target,
                Translations = translations,
                Examples = examples,
            };
        }

        #region Private Methods

        private string GetInputFromHtml()
        {
            return _html.DocumentNode
                .SelectSingleNode("//*[@id='search-input']//input[1]")
                .GetAttributeValue("value", string.Empty);
        }

        private (Language Source, Language Target) GetLanguagesFromHtml()
        {
            var source = _html.DocumentNode
                .SelectSingleNode("//*[@id='src-selector']//span[@class='option front']")
                .GetAttributeValue("data-value", string.Empty)
                .ToLanguageFromShortName();

            var target = _html.DocumentNode
                .SelectSingleNode("//*[@id='trg-selector']//span[@class='option front']")
                .GetAttributeValue("data-value", string.Empty)
                .ToLanguageFromShortName();

            return (source, target);
        }

        private IEnumerable<Word> GetTranslationsFromHtml(Language targetLanguage)
        {
            return _html.DocumentNode
                .SelectNodes("//*[@id='translations-content']//span[@class='display-term']")
                ?.Select(n => new Word(n.InnerHtml.ReplaceSpecSymbols(), targetLanguage, GetPartOfSpeech(n)));

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

        private IEnumerable<Example> GetExamplesFromHtml(Language sourceLanguage, Language targetLanguage)
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

            for (var i = 0; i < targetSentences.Count(); i++)
            {
                var sourceSentence = new Sentence(sourceLanguage, sourceSentences.ElementAt(i));
                var targetSentence = new Sentence(targetLanguage, targetSentences.ElementAt(i));
                yield return new Example(sourceSentence, targetSentence);
            }
        }

        private string GetLayout(Language language)
        {
            return language == Language.Arabic ? $"rtl {Language.Arabic.ToString().ToLower()}" :
                   language == Language.Hebrew ? "rtl" :
                   "ltr";
        }

        #endregion
    }
}
