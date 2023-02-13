namespace ReversoAPI.Web.Models.Values
{
    public class Example
    {
        public Example(Sentence source, Sentence target)
        {
            Source = source;
            Target = target;
        }

        public Sentence Source { get; }
        public Sentence Target { get; }
    }
}
