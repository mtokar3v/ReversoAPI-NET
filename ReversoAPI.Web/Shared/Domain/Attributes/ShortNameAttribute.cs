using System;

namespace ReversoAPI
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
