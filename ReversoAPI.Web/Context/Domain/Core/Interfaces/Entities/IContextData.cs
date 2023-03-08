using ReversoAPI.Web.Context.Domain.Core.ValueObjects;
using ReversoAPI.Web.Shared.Domain.ValueObjects;
using System.Collections.Generic;

namespace ReversoAPI.Web.Context.Domain.Core.Interfaces.Entities
{
    public interface IContextData
    {
        IEnumerable<Example> Examples { get; set; }
        Language Source { get; set; }
        Language Target { get; set; }
        string Text { get; set; }
    }
}