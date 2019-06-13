using System;
using System.Collections.Generic;
using System.Text;

namespace TStack.ADO.Connection
{
    public class ADOConnection
    {
        public ADOConnection(string connStr)
        {
            if (string.IsNullOrEmpty(connStr))
                throw new ArgumentNullException("Connection string must not be null or empty.");
            ConnectionString = connStr;
        }
        internal string ConnectionString { get; private set; }
    }
}
