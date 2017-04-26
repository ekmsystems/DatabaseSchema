# DatabaseSchema

Library for extracting the schema from a database.

[![Build status](https://ci.appveyor.com/api/projects/status/oir92a7cwtys3dty?svg=true)](https://ci.appveyor.com/project/mrstebo/databaseschema)
[![Coverage Status](https://coveralls.io/repos/github/ekmsystems/DatabaseSchema/badge.svg?branch=master)](https://coveralls.io/github/ekmsystems/DatabaseSchema?branch=master)
[![NuGet](https://img.shields.io/nuget/v/DatabaseSchema.svg)](https://www.nuget.org/packages/DatabaseSchema/)

DatabaseSchema is available via [NuGet](https://www.nuget.org/packages/DatabaseSchema/):

```powershell
Install-Package DatabaseSchema
```

## Quick start

Here is a simple bit of code showing how to create and use an instance of the `IDbSchemaReader`.

```cs
using(var connection = new OleDbConnection("Data Source=MyDatabase.mdb;Provider=Microsoft.Jet.OLEDB.4.0;"))
{
    var schemaReader = new OleDbSchemaReader(connection);
    var schemas = schemaReader.GetSchemas();
    var schema = schemaReader.GetSchema("MyTable");
    // ...
}
```
