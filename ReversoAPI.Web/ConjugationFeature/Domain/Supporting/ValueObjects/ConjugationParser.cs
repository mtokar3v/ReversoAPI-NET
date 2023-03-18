using HtmlAgilityPack;
using System.Collections.Generic;

namespace ReversoAPI.Web.ConjugationFeature.Domain.Supporting.ValueObjects
{
    public abstract class ConjugationParser
    {
        protected readonly HtmlDocument _html;
        protected readonly Language _language;
        public ConjugationParser(HtmlDocument html, Language language)
        {
            _html = html;
            _language = language;
        }

        public abstract Dictionary<string, IEnumerable<Conjugation>> Parse();

        protected string GetXPathGrid(int row, int col) => $"{XPathWrapper}[{row}]/div[@class='wrap-three-col'][{col}]/div[contains(@class, 'blue-box-wrap')]";
        protected string XPathWrapper => "//div[contains(@class, 'word-wrap-row')]";
    }
}
