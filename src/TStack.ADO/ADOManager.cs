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
            _sqlConnection = new SqlConnection(_adoConnection.ConnectionString);
        }
    }
    public abstract class ADOManager : ADOBaseManager
    {
        public ADOManager(ADOConnection connection) : base(connection)
        {
        }
        public ADOManager()
        {
            throw new ArgumentNullException("connection must not be null");
        }
        public T ExecuteScalar<T>(string command, TStack.ADO.Tool.CommandType commandType, List<Parameter> parameters = null)
            where T : struct
        {
            var sqlCommand = CommandBuilder(command, commandType, parameters);
            return ExecuteScalar<T>(sqlCommand);
        }
        public void Execute(string command, TStack.ADO.Tool.CommandType commandType, List<Parameter> parameters = null)
        {
            var sqlCommand = CommandBuilder(command, commandType, parameters);
            Execute(sqlCommand);
        }
        public T Execute<T>(string command, TStack.ADO.Tool.CommandType commandType, List<Parameter> parameters = null)
        {
            var sqlCommand = CommandBuilder(command, commandType, parameters);
            return Execute<T>(sqlCommand);
        }
        public DataTable GetDataTable(string command, TStack.ADO.Tool.CommandType commandType, List<Parameter> parameters = null)
        {
            var sqlCommand = CommandBuilder(command, commandType, parameters);
            return GetDataTable(sqlCommand);
        }
        public DataSet GetDataSet(string command, TStack.ADO.Tool.CommandType commandType, List<Parameter> parameters = null)
        {
            var sqlCommand = CommandBuilder(command, commandType, parameters);
            return GetDataSet(sqlCommand);
        }
    }
}
