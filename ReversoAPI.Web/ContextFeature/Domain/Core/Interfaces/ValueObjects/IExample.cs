namespace ReversoAPI.Web.ContextFeature.Domain.Core.Interfaces.ValueObjects
{
    public interface IExample
    {
        ISentence Source { get; }
        ISentence Target { get; }
    }
}