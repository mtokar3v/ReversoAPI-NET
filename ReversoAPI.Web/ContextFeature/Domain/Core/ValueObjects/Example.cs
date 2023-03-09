using ReversoAPI.Web.ContextFeature.Domain.Core.Interfaces.ValueObjects;

namespace ReversoAPI.Web.ContextFeature.Domain.Core.ValueObjects
{
    public class Example : IExample
    {
        public Example(ISentence source, ISentence target)
        {
            Source = source;
            Target = target;
        }

        public ISentence Source { get; }
        public ISentence Target { get; }
    }
}
