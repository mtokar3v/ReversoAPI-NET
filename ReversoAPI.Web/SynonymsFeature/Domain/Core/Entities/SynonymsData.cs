using System.Collections.Generic;
using ReversoAPI.Web.SynonymsFeature.Domain.Core.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.SynonymsFeature.Domain.Core.Entities
{
    public class SynonymsData
    {
        public string Text { get; set; }
        public Language Language { get; set; }
        public IEnumerable<Synonim> Synonyms { get; set; }
    }
}
