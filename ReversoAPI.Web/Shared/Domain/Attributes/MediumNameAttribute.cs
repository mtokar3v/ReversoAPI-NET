using System;

namespace ReversoAPI.Web.Shared.Domain.Attributes
{
    public class MediumNameAttribute : Attribute
    {
        public string Name { get; set; }
        public MediumNameAttribute(string name)
        {
            Name = name;
        }
    }
}
