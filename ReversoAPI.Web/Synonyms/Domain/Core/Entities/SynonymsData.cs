using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.Synonyms.Domain.ValueObjects;
using System.Collections.Generic;

namespace ReversoAPI.Web.Synonyms.Domain.Core.Entities
{
    public class SynonymsData : ISynonymsData
    {
        public string Text { get; set; }
        public Language Language { get; set; }
        public IEnumerable<Synonim> Synonyms { get; set; }
    }
}
