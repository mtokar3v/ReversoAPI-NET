using System;

namespace ReversoAPI
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
