using System.Collections.Generic;
using ReversoAPI.Web.GrammarCheckFeature.Domain.Interfaces.Entities;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.GrammarCheckFeature.Domain.Entities
{
    public class SpellingData : ISpellingData
    {
        public string Text { get; set; }
        public Language Language { get; set; }
        public IEnumerable<ICorrection> Correction { get; set; }
    }
}
