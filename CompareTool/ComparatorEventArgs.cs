using System;
using System.Collections.Generic;

namespace CompareTool
{
    public class ComparatorEventArgs : EventArgs
    {
        public bool IsSuccess { get; set; }

        public List<string> Dis { get; set; }
    }
}