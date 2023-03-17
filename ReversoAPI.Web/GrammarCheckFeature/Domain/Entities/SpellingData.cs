using System.Collections.Generic;
using ReversoAPI.Web.GrammarCheckFeature.Domain.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.GrammarCheckFeature.Domain.Entities
{
    public class SpellingData
    {
        public string Text { get; set; }
        public Language Language { get; set; }
        public IEnumerable<Correction> Correction { get; set; }
    }
}
