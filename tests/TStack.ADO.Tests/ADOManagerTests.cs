using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using TStack.ADO.Tool;
using Xunit;

namespace TStack.ADO.Tests
{
    public class ADOManagerTests
    {
        private SQLManager _sqlManager;
        public ADOManagerTests()
        {
            _sqlManager = new SQLManager(new TestConnection());
        }
        [Fact]
        public void Execute_should_create_read_update_delete_CRUD()
        {
            string query = "";
            List<Parameter> parameters = new List<Parameter>();


            //CREATE
            query = "Insert into Employee (Name,Surname,Email,BirthDate,Salary) values (@Name,@Surname,@Email,@BirthDate,@Salary)";
            parameters.Add("Name", "Ferhat");
            parameters.Add("Surname", "Candas");
            parameters.Add("Email", "candasferhat61@gmail.com");
            parameters.Add("BirthDate", new DateTime(1992, 07, 24));
            parameters.Add("Salary", 4023.53f);
            _sqlManager.Execute(query, CommandType.Text, parameters);
            //CREATE


            //READ
            query = "Select * from Employee order by Id desc";
            var dataTable = _sqlManager.GetDataTable(query, CommandType.Text);
            dataTable.Rows[0]["Name"].Should().Be("Ferhat");
            dataTable.Rows[0]["Surname"].Should().Be("Candas");
            dataTable.Rows[0]["Email"].Should().Be("candasferhat61@gmail.com");
            dataTable.Rows[0]["BirthDate"].Should().Be(new DateTime(1992, 07, 24));
            dataTable.Rows[0]["Salary"].Should().Be(4023.53f);
            //READ

            //UPDATE
            query = "UPDATE Employee set Salary=@Salary where Id=@Id";
            parameters = new List<Parameter>();
            parameters.Add("Salary", 3267.78d);
            parameters.Add("Id", dataTable.Rows[0]["Id"]);
            _sqlManager.Execute(query, CommandType.Text, parameters);
            //UPDATE


            //READ
            query = "Select Salary from Employee order by Id desc";
            double salary = _sqlManager.ExecuteScalar<double>(query, CommandType.Text);
            salary.Should().Be(3267.78d);
            //READ




            //DELETE
            query = "Delete Employee where name='Ferhat'";
            _sqlManager.Execute(query, CommandType.Text);
            //DELETE

            //READ
            query = "SELECT COUNT(*) FROM Employee";
            var expected = _sqlManager.ExecuteScalar<int>(query, Tool.CommandType.Text);
            expected.Should().Be(0);
            //READ

        }
    }
}
