# Overview

The purpose of this library is the **CRUD** operations on the mssql server and also to work with a clean architecture. This library was developed with .NET standard.

## Usage
Before usage test project create manually database table like on picture 

![](documents/ADO.NET_TESTDB.png)

#### Step One
Inherit the ADOConnection class to the new class you are creating and set up the database connection.

```csharp
  public class TestConnection : ADOConnection
    {
        public TestConnection() : base(@"Server=.\SQLEXPRESS;Database=TESTDB;Trusted_Connection=True;")
        {
        }
    }
```

#### Step Two
ADOManager is an abstract class, so it must be inherited to your new class, in which case all methods can be used
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
This parameter is the reference type **string**, if the entered query is **stored procedure**, the [CommandType](#commandtype) parameter should be selected as "StoredProcedure", otherwise "Text"
#### CommandType
This parameter is the reference type enum and have two choice
 - Text
 - StoredProcedure
 
#### Parameter
This parameter is reference type List of Parameter class and for usage has two parameter "key" (string,not nullable), "value" (object, not nullable)
Example Usage :
```csharp
 List<Parameter> parameters = new List<Parameter>();
 parameters.Add("Name", "Ferhat");
```
**Note :** The @ character is not required in the entered key parameter. No problem if entered.
```
parameters.Add("Name", "Ferhat");  Allowed
parameters.Add("@Name", "Ferhat"); Allowed
```

# Author

Ferhat Candaş - Software Developer
 - Mail : candasferhat61@gmail.com
 - LinkedIn : https://www.linkedin.com/in/ferhatcandas
