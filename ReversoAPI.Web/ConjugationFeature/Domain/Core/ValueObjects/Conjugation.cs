namespace ReversoAPI
{
    public class Conjugation
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
