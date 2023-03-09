using System.Collections.Generic;
using ReversoAPI.Web.TranslationFeature.Domain.Interfaces.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.TranslationFeature.Domain.Interfaces.Entities
{
    public interface ITranslationData
    {
        Language Source { get; set; }
        Language Target { get; set; }
        string Text { get; set; }
        IEnumerable<ITranslation> Translations { get; set; }
    }
}