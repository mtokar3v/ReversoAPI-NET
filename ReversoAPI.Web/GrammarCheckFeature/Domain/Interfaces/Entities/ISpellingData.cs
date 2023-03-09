using System.Collections.Generic;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.GrammarCheckFeature.Domain.Interfaces.Entities
{
    public interface ISpellingData
    {
        IEnumerable<ICorrection> Correction { get; set; }
        Language Language { get; set; }
        string Text { get; set; }
    }
}