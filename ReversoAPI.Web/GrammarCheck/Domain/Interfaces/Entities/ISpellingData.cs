using ReversoAPI.Web.GrammarCheck.Domain.Entities;
using ReversoAPI.Web.Shared.Domain.ValueObjects;
using System.Collections.Generic;

namespace ReversoAPI.Web.GrammarCheck.Domain.Interfaces.Entities
{
    public interface ISpellingData
    {
        IEnumerable<Correction> Correction { get; set; }
        Language Language { get; set; }
        string Text { get; set; }
    }
}