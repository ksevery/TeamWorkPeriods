using System;
using System.Collections.Generic;
using System.Text;

namespace TeamWorkhours.Engine.Converters
{
    public class ConvertException : Exception
    {
        public ConvertException(string message, Exception ex)
            : base(message, ex) { }
    }
}
