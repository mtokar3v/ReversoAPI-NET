using ReversoAPI.Web.Shared.Domain.ValueObjects;
using ReversoAPI.Web.Values;
using System.Collections.Generic;

namespace ReversoAPI.Web.Translation.Domain.Entities
{
    public class TranslationData : ITranslationData
    {
        public string Text { get; set; }
        public Language Source { get; set; }
        public Language Target { get; set; }
        public IEnumerable<Translation> Translations { get; set; }
    }
}
