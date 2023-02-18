namespace ReversoAPI.Web.Values
{
    public class Correction
    {
        public Correction(
            string correctedText,
            string mistakeText,
            int startIndex,
            int endIndex,
            string shortDescription,
            string longDescription)
        {
            CorrectedText = correctedText;
            MistakeText = mistakeText;
            StartIndex = startIndex;
            EndIndex = endIndex;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
        }

        public string CorrectedText { get; set; }
        public string MistakeText { get; set; }

        public int StartIndex { get; set; }
        public int EndIndex { get; set; }

        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
    }
}
