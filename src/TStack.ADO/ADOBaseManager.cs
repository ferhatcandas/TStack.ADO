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
            try
            {
                Open(_sqlConnection);
                T response = action();
                Close(_sqlConnection);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        private void SqlProcess(Action action)
        {
            try
            {
                Open(_sqlConnection);
                action();
                Close(_sqlConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
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
        // internal T Execute<T>(SqlCommand command)
        // {
        //     return SqlProcess<T>(() =>
        //     {
        //         return Parse<T>(command.ExecuteNonQuery());
        //     });
        // }
        internal void Execute(SqlCommand command)
        {
            SqlProcess(() =>
           {
               command.ExecuteNonQuery();
           });
        }
        internal T ExecuteScalar<T>(SqlCommand command)
            where T : struct
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
}
