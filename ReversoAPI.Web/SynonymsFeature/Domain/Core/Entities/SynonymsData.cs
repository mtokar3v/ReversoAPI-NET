using System.Collections.Generic;
using ReversoAPI.Web.SynonymsFeature.Domain.Core.Interfaces.ValueObjects;
using ReversoAPI.Web.SynonymsFeature.Domain.Core.Interfaces.Entities;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.SynonymsFeature.Domain.Core.Entities
{
    public class SynonymsData : ISynonymsData
    {
        public string Text { get; set; }
        public Language Language { get; set; }
        public IEnumerable<ISynonim> Synonyms { get; set; }
    }
}
