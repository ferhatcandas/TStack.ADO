# TStack.ADO

This library was developed with .NET standard.

## Usage

#### Step One
For usage is inheritance ADOConnection to new class and set connection to access database

```csharp
  public class TestConnection : ADOConnection
    {
        public TestConnection() : base(@"Server=.\SQLEXPRESS;Database=TESTDB;Trusted_Connection=True;")
        {
        }
    }
```

#### Step Two
ADOManager class is abstarct class so create new manager to use all methods.
```csharp
 public class SQLManager : ADOManager
    {
        public SQLManager(TestConnection connection) : base(connection)
        {
        }
    }
```
That's it, ready to use.

## Library Fundamentals
For use methods first learn what is parameters and features, lets look.
#### Query
This parameter must be **string** includes **stored procedure** name or **t-sql** query when input is stored procedure name than [CommandType](#commandtype) value set to StoredProcedure other wise Text
#### CommandType

#### Paramter
