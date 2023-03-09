using System.Collections.Generic;
using ReversoAPI.Web.GrammarCheckFeature.Domain.Interfaces.Entities;

namespace ReversoAPI.Web.GrammarCheckFeature.Domain.Entities
{
    public class Correction : ICorrection
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
