using System.Collections.Generic;
using ReversoAPI.Web.ContextFeature.Domain.Core.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.ContextFeature.Domain.Core.Enities
{
    public class ContextData
    {
        public string Text { get; set; }

        public Language Source { get; set; }
        public Language Target { get; set; }

        public IEnumerable<Example> Examples { get; set; }
    }
}
