using System.Collections.Generic;

namespace ReversoAPI.Web.GrammarCheck.Domain.Entities
{
    // Is it entity or value object?
    public class Correction : ICorrection
    {
        public Correction()
        {
        }

        public Correction(string correctedText, string mistakeText)
        {
            CorrectedText = correctedText;
            MistakeText = mistakeText;
        }

        public string CorrectedText { get; set; }
        public string MistakeText { get; set; }

        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

        public IEnumerable<string> Suggestions { get; set; }
    }
}
