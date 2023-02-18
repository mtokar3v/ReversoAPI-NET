using ReversoAPI.Web.Values;
using System.Collections.Generic;

namespace ReversoAPI.Web.Entities
{
    public class SynonymsData
    {
        public string Text { get; set; }
        public Language Source { get; set; }
        public IEnumerable<Word> Synonyms { get; set; }
    }
}
