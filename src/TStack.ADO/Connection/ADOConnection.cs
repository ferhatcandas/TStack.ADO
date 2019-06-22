using System;
using System.Collections.Generic;
using System.Text;

namespace TStack.ADO.Connection
{
    public class ADOConnection
    {
        /// <summary>
        /// ex : Server=.\SQLEXPRESS;Database=TESTDB;Trusted_Connection=True;
        /// </summary>
        /// <param name="connStr"></param>
        public ADOConnection(string connStr)
        {
            if (string.IsNullOrEmpty(connStr))
                throw new ArgumentNullException("Connection string must not be null or empty.");
            ConnectionString = connStr;
        }
        internal string ConnectionString { get; private set; }
    }
}
