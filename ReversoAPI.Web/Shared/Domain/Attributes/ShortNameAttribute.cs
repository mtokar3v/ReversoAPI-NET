using System;

namespace ReversoAPI.Web.Shared.Domain.Attributes
{
    public class ShortNameAttribute : Attribute
    {
        public string Name { get; set; }
        public ShortNameAttribute(string name)
        {
            Name = name;
        }
    }
}
