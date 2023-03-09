using ReversoAPI.Web.ContextFeature.Domain.Core.Interfaces.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.ContextFeature.Domain.Core.ValueObjects
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
