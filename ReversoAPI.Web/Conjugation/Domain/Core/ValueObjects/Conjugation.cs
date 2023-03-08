using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.Conjugation.Domain.Core.ValueObjects
{
    public class Conjugation : IConjugation
    {
        public Conjugation(string group, string verb, Language language)
        {
            Group = group;
            Verb = verb;
            Language = language;
        }

        public Language Language { get; }
        public string Group { get; }
        public string Verb { get; }
    }
}
