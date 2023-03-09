using System.Collections.Generic;
using ReversoAPI.Web.ContextFeature.Domain.Core.Interfaces.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;

namespace ReversoAPI.Web.ContextFeature.Domain.Core.Interfaces.Entities
{
    public interface IContextData
    {
        IEnumerable<IExample> Examples { get; set; }
        Language Source { get; set; }
        Language Target { get; set; }
        string Text { get; set; }
    }
}