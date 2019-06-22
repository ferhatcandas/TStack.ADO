# TStack.ADO

This library was developed with .NET standard.

## Usage
Before usage test project create manually database and table like on picture 

![](documents/ADO.NET_TESTDB.png)

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
This parameter reference type is **string** includes **stored procedure** name or **t-sql** query when input is stored procedure name than [CommandType](#commandtype) value set to StoredProcedure other wise Text
#### CommandType
This paramater reference type is enum and have two choice
 - Text
 - StoredProcedure
 
#### Paramter
This paramater reference type is List of Parameter class and for usage has two paramter key(string,not nullable), value(object, not nullable)
Example Usage :
```csharp
 List<Parameter> parameters = new List<Parameter>();
 parameters.Add("Name", "Ferhat");
```
**Note :** On input key parameter not required start with @ character, but if you can input this character library detect your character and dont repeat this character.
```
parameters.Add("Name", "Ferhat");    Allowed
parameters.Add("@Name", "Ferhat"); Allowed
```