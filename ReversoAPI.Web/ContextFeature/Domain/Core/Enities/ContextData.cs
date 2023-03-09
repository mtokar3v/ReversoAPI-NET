using System.Collections.Generic;
using ReversoAPI.Web.ContextFeature.Domain.Core.Interfaces.Entities;
using ReversoAPI.Web.ContextFeature.Domain.Core.Interfaces.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.ContextFeature.Domain.Core.Enities
{
    public class ContextData : IContextData
    {
        public string Text { get; set; }

        public Language Source { get; set; }
        public Language Target { get; set; }

        public IEnumerable<IExample> Examples { get; set; }
    }
}
