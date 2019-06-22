using System;
using System.Collections.Generic;
using System.Text;

namespace TStack.ADO.Tool
{
    public class Parameter
    {
        public Parameter(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            Key = key.StartsWith("@") ? key : "@" + key;
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            Value = value;
        }
        internal string Key { get; private set; }
        internal object Value { get; private set; }
    }
}
