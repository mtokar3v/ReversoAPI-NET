using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.ContextFeature.Domain.Core.Interfaces.ValueObjects
{
    public interface ISentence
    {
        Language Language { get; }
        string Text { get; }
    }
}