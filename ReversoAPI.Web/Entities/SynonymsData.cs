using ReversoAPI.Web.Values;
using System.Collections.Generic;

namespace ReversoAPI.Web.Entities
{
    public class SynonymsData
    {
        public string Text { get; set; }
        public Language Language { get; set; }
        public IEnumerable<Synonim> Synonyms { get; set; }
    }
}
