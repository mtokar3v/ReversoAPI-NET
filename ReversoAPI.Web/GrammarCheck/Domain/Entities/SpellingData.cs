using ReversoAPI.Web.Entities;
using ReversoAPI.Web.Shared.Domain.ValueObjects;
using System.Collections.Generic;

namespace ReversoAPI.Web.GrammarCheck.Domain.Entities
{
    public class SpellingData : ISpellingData
    {
        public string Text { get; set; }
        public Language Language { get; set; }
        public IEnumerable<Correction> Correction { get; set; }
    }
}
