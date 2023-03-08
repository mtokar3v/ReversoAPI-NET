using ReversoAPI.Web.Context.Domain.Core.ValueObjects;

namespace ReversoAPI.Web.Context.Domain.Core.Interfaces.ValueObjects
{
    public interface IExample
    {
        Sentence Source { get; }
        Sentence Target { get; }
    }
}