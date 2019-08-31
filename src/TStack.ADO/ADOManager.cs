using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TStack.ADO;
using TStack.ADO.Connection;
using TStack.ADO.Tool;

namespace TStack.ADO
{
    public abstract class ADOManager<TContext> : ADOManager
        where TContext : ADOConnection, new()
    {
        internal ADOConnection _adoConnection;
        public ADOManager()
        {
            _adoConnection = new TContext();
            if (string.IsNullOrEmpty(_adoConnection.ConnectionString))
                throw new ArgumentNullException();
            if (_sqlConnection == null)
                _sqlConnection = new SqlConnection(_adoConnection.ConnectionString);
        }
    }
    public class ADOManager : ADOBaseManager
    {
        public ADOManager(ADOConnection connection) : base(connection)
        {
        }
        public ADOManager() => throw new ArgumentNullException("connection must not be null");

        public T ExecuteScalar<T>(string command, Tool.CommandType commandType, List<Parameter> parameters = null)
            where T : struct => ExecuteScalar<T>(CommandBuilder(command, commandType, parameters));

        public string ExecuteScalar(string command, Tool.CommandType commandType, List<Parameter> parameters = null)
        => ExecuteScalar<string>(CommandBuilder(command, commandType, parameters));

        public void Execute(string command, Tool.CommandType commandType, List<Parameter> parameters = null) => Execute(CommandBuilder(command, commandType, parameters));

        public DataTable GetDataTable(string command, Tool.CommandType commandType, List<Parameter> parameters = null) => GetDataTable(CommandBuilder(command, commandType, parameters));

        public DataSet GetDataSet(string command, Tool.CommandType commandType, List<Parameter> parameters = null) => GetDataSet(CommandBuilder(command, commandType, parameters));
    }
}
