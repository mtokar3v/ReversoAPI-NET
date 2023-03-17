using System.Collections.Generic;

namespace ReversoAPI.Web.GrammarCheckFeature.Domain.ValueObjects
{
    public class Correction
    {
        public Correction(
            string correctedText,
            string mistakeText,
            int startIndex,
            int endIndex,
            string shortDescription,
            string longDescription,
            IEnumerable<string> suggestions)
        {
            CorrectedText = correctedText;
            MistakeText = mistakeText;
            StartIndex = startIndex;
            EndIndex = endIndex;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
            Suggestions = suggestions;
        }

        public string CorrectedText { get; }
        public string MistakeText { get; }

        public int StartIndex { get; }
        public int EndIndex { get; }

        public string ShortDescription { get; }
        public string LongDescription { get; }

        public IEnumerable<string> Suggestions { get; }
    }
}
