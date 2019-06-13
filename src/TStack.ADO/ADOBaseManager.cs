using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TStack.ADO.Connection;
using TStack.ADO.Tool;

namespace TStack.ADO
{
    public abstract class ADOBaseManager
    {
        internal SqlConnection _sqlConnection;
        internal SqlCommand _sqlCommand;
        internal SqlDataAdapter _sqlDataAdapter;
        internal SqlDataReader _sqlDataReader;
        internal DataTable dataTable;
        internal DataSet dataSet;
        public ADOBaseManager(ADOConnection connection)
        {
            if (string.IsNullOrEmpty(connection.ConnectionString))
                throw new ArgumentNullException();
            _sqlConnection = new SqlConnection(connection.ConnectionString);
        }
        public ADOBaseManager()
        {

        }
        private T SqlProcess<T>(Func<T> action)
        {
            Open(_sqlConnection);
            T response = action();
            Close(_sqlConnection);
            return response;
        }
        private void SqlProcess(Action action)
        {
            Open(_sqlConnection);
            action();
            Close(_sqlConnection);
        }
        internal T Parse<T>(object value)
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Open(SqlConnection sqlConnection)
        {
            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();
        }
        private void Close(SqlConnection sqlConnection)
        {
            if (sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }
        internal SqlCommand CommandBuilder(string command, Tool.CommandType commandType, List<Parameter> parameters)
        {
            _sqlCommand = new SqlCommand(command, _sqlConnection);
            _sqlCommand.AddParameters(parameters);
            _sqlCommand.ParseCommandType(commandType);
            return _sqlCommand;
        }
        internal T Execute<T>(SqlCommand command)
        {
            return SqlProcess<T>(() =>
            {
                return Parse<T>(command.ExecuteNonQuery());
            });
        }
        internal void Execute(SqlCommand command)
        {
            SqlProcess(() =>
           {
               command.ExecuteNonQuery();
           });
        }
        internal T ExecuteScalar<T>(SqlCommand command)
        {
            return SqlProcess<T>(() =>
            {
                return Parse<T>(command.ExecuteScalar());
            });
        }
        internal DataTable GetDataTable(SqlCommand sqlCommand)
        {
            return SqlProcess<DataTable>(() =>
            {
                _sqlDataAdapter = GetDataAdapter(sqlCommand);
                dataTable = new DataTable();
                _sqlDataAdapter.Fill(dataTable);
                return dataTable;
            });
        }
        internal DataSet GetDataSet(SqlCommand sqlCommand)
        {
            return SqlProcess<DataSet>(() =>
            {
                _sqlDataAdapter = GetDataAdapter(sqlCommand);
                dataSet = new DataSet();
                _sqlDataAdapter.Fill(dataSet);
                return dataSet;
            });
        }
        private SqlDataAdapter GetDataAdapter(SqlCommand sqlCommand)
        {
            _sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            return _sqlDataAdapter;
        }
        internal SqlDataReader GetDataReader(SqlCommand sqlCommand)
        {
            _sqlDataReader = sqlCommand.ExecuteReader();
            return _sqlDataReader;
        }
    }
    internal static class ADOExtension
    {
        internal static void AddParameters(this SqlCommand command, List<Parameter> parameters)
        {
            foreach (Parameter param in parameters)
                command.Parameters.AddWithValue("@" + param.Key, param.Value);
        }
        internal static void ParseCommandType(this SqlCommand command, Tool.CommandType commandType)
        {
            command.CommandType = (System.Data.CommandType)Enum.Parse(typeof(System.Data.CommandType), commandType.ToString());
        }
    }
}
