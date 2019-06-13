using System;
using System.Collections.Generic;
using System.Text;

namespace TStack.ADO.Tool
{
    public class Parameter
    {
        public Parameter(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
        }
        internal string Key { get; private set; }
        internal object Value { get; private set; }
    }
}
