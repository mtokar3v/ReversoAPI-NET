using System.Collections.Generic;
using ReversoAPI.Web.SynonymsFeature.Domain.Core.Interfaces.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;


namespace ReversoAPI.Web.SynonymsFeature.Domain.Core.Interfaces.Entities
{
    public interface ISynonymsData
    {
        Language Language { get; set; }
        IEnumerable<ISynonim> Synonyms { get; set; }
        string Text { get; set; }
    }
}