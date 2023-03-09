using System.Collections.Generic;
using ReversoAPI.Web.TranslationFeature.Domain.Interfaces.Entities;
using ReversoAPI.Web.TranslationFeature.Domain.Interfaces.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.TranslationFeature.Domain.Entities
{
    public class TranslationData : ITranslationData
    {
        public string Text { get; set; }
        public Language Source { get; set; }
        public Language Target { get; set; }
        public IEnumerable<ITranslation> Translations { get; set; }
    }
}
