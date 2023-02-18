using ReversoAPI.Web.Values;
using System.Collections.Generic;

namespace ReversoAPI.Web.Entities
{
    public class ContextData
    {
        public string Text { get; set; }

        public Language Source { get; set; }
        public Language Target { get; set; }

        public IEnumerable<Word> Translations { get; set; } // may be rid this?
        public IEnumerable<Example> Examples { get; set; }
    }
}
