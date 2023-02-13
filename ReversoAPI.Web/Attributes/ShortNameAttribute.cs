using System;
using System.Collections.Generic;
using System.Text;

namespace ReversoAPI.Web.Attributes
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
