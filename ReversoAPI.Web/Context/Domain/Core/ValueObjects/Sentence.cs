namespace ReversoAPI.Web.Context.Domain.Core.ValueObjects
{
    public class Sentence : ISentence
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
