using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.Synonyms.Domain.ValueObjects;
using System.Collections.Generic;

namespace ReversoAPI.Web.Synonyms.Domain.Core.Interfaces.Entities
{
    public interface ISynonymsData
    {
        Language Language { get; set; }
        IEnumerable<Synonim> Synonyms { get; set; }
        string Text { get; set; }
    }
}