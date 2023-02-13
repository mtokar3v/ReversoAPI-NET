using ReversoAPI.Web.Extensions;
using ReversoAPI.Web.Models.Entities;
using ReversoAPI.Web.Models.Responses;
using ReversoAPI.Web.Models.Values;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReversoAPI.Web.Tools.Parsers
{
    public class SynonymsResponseParser : BaseResponseParser<SynonymsResponse>
    {
        public SynonymsResponseParser(string html) : base(html) { }

        public override SynonymsResponse Invoke()
        {
            var language = GetLanguage();
            var text = GetText();
            var synonims = GetSynonims(language);

            return new SynonymsResponse
            {
                Source = language,
                Text = text,
                Synonyms = synonims,
            };
        }

        private Language GetLanguage()
        {
            return _html.DocumentNode.SelectSingleNode("//*[@id='trg-selector']//span[@class='lang-trg'][1]").InnerHtml.ToLanguage();
        }

        public string GetText()
        {
            return _html.DocumentNode.SelectSingleNode("//div[@class='search-title']").InnerHtml;
        }

        private IEnumerable<Word> GetSynonims(Language language)
        {
            var partsOfSpeech = _html.DocumentNode
                .SelectNodes("//*[@class='wrap-hold-prop']//div/h2")
                .Select(n => n.InnerHtml.ToPartOfSpeech());


            foreach (var partOfSpeech in partsOfSpeech.Select((value, index) => new { index, value }))
            {
                var synonims = _html.DocumentNode
                    .SelectNodes($"//div[@class='wrap-hold-prop'][{partOfSpeech.index + 1}]//a[@class='synonym  relevant' or @class='synonym ']")
                    .Select(n => n.InnerHtml.ReplaceSpecSymbols());

                foreach (var s in synonims)
                {
                    Console.WriteLine(s);
                    yield return new Word(s, language, partOfSpeech.value);
                }
            }
        }
    }
}
