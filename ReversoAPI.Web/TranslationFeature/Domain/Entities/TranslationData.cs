using System.Collections.Generic;
using ReversoAPI.Web.TranslationFeature.Domain.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.TranslationFeature.Domain.Entities
{
    public class TranslationData
    {
        public string Text { get; set; }
        public Language Source { get; set; }
        public Language Target { get; set; }
        public IEnumerable<Translation> Translations { get; set; }
    }
}
