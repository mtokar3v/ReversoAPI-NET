using ReversoAPI.Web.Models.Entities;
using ReversoAPI.Web.Models.Values;
using System.Collections.Generic;

namespace ReversoAPI.Web.Models.Responses
{
    public class SynonymsResponse
    {
        public string Text { get; set; }
        public Language Source { get; set; }
        public IEnumerable<Word> Synonyms { get; set; }
    }
}
