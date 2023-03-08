using ReversoAPI.Web.Context.Domain.Core.Context.ValueObjects;
using ReversoAPI.Web.Context.Domain.Generic.ValueObjects;
using System.Collections.Generic;

namespace ReversoAPI.Web.Context.Domain.Core.Enities
{
    public class ContextData : IContextData
    {
        public string Text { get; set; }

        public Language Source { get; set; }
        public Language Target { get; set; }

        public IEnumerable<Example> Examples { get; set; }
    }
}
