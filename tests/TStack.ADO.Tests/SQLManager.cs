using System;
using System.Collections.Generic;
using System.Text;
using TStack.ADO.Connection;

namespace TStack.ADO.Tests
{
    public class SQLManager : ADOManager
    {
        public SQLManager(TestConnection connection) : base(connection)
        {
        }
    }
}
