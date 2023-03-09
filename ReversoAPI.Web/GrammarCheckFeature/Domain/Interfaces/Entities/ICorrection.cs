using System.Collections.Generic;

namespace ReversoAPI.Web.GrammarCheckFeature.Domain.Interfaces.Entities
{
    public interface ICorrection
    {
        string CorrectedText { get; }
        int EndIndex { get; }
        string LongDescription { get; }
        string MistakeText { get; }
        string ShortDescription { get; }
        int StartIndex { get; }
        IEnumerable<string> Suggestions { get; }
    }
}