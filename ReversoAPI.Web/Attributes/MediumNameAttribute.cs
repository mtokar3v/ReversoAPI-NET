using System;

namespace ReversoAPI.Web.Attributes
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
