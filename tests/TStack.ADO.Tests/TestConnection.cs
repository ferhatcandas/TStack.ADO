using System;
using System.Collections.Generic;
using System.Text;
using TStack.ADO.Connection;

namespace TStack.ADO.Tests
{
    public class TestConnection : ADOConnection
    {
        public TestConnection() : base(@"Server=.\SQLEXPRESS;Database=TESTDB;Trusted_Connection=True;")
        {
        }
    }
}
