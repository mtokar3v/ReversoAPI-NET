using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.Translation.Domain.Interfaces.Entities
{
    public interface ITranslationData
    {
        Language Source { get; set; }
        Language Target { get; set; }
        string Text { get; set; }
        System.Collections.Generic.IEnumerable<Translation> Translations { get; set; }
    }
}