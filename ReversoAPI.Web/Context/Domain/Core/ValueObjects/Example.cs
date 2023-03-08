namespace ReversoAPI.Web.Context.Domain.Core.ValueObjects
{
    public class Example : IExample
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
