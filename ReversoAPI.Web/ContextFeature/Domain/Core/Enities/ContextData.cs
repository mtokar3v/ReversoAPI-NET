﻿using System.Collections.Generic;

namespace ReversoAPI
{
    public class ContextData
    {
        public string Text { get; set; }

        public Language Source { get; set; }
        public Language Target { get; set; }

        public IEnumerable<Example> Examples { get; set; }
    }
}
