using ReversoAPI.Web.Models.Entities;
using ReversoAPI.Web.Models.Values;
using System.Collections.Generic;

namespace ReversoAPI.Web.Models.Responses
{
    public class ContextResponse
    {
        public string Text { get; set; }
        public Language Source { get; set; }
        public Language Target { get; set; }
        public IEnumerable<Word> Translations { get; set; }
        public IEnumerable<Example> Examples { get;set; }
    }
}
