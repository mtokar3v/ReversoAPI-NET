using System.Collections.Generic;

namespace ReversoAPI.Web.GrammarCheck.Domain.Interfaces.Entities
{
    public interface ICorrection
    {
        string CorrectedText { get; set; }
        int EndIndex { get; set; }
        string LongDescription { get; set; }
        string MistakeText { get; set; }
        string ShortDescription { get; set; }
        int StartIndex { get; set; }
        IEnumerable<string> Suggestions { get; set; }
    }
}