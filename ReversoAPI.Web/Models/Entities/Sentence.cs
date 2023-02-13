namespace ReversoAPI.Web.Models.Values
{
    public class Sentence
    {
        public Sentence(Language language, string text)
        {
            Language = language;
            Text = text;
        }

        public Language Language { get; }
        public string Text { get; }
    }
}
