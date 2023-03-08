namespace ReversoAPI.Web.Context.Domain.Core.Interfaces.ValueObjects
{
    public interface ISentence
    {
        Language Language { get; }
        string Text { get; }
    }
}