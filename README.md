# osquery-csharp
## Overview
This project contains the C# bindings for creating osquery extensions in C#. The extension can register table, config or logger plugins.
Plugin can quickly enable the integration of data which is not yet available as a part of base osquery. 

## Prerequisites
Osquery must be installed on the computer you are running this software. Osquery should be run as the same user the user which runs the code shown here.

## How to
**Consider the following example:**
```c#

using System;
using System.Collections.Generic;
using osquery_csharp.osquery;

public class MyTablePlugin: TablePlugin {
   
    public override List<TableColumn<string, string>> columns() {
		var colList = new List<TableColumn<string, string>>();
		var col1 = new TableColumn<string,string>()
        {
            {"foo", "TEXT"}
        };
		colList.Add(col1);

		var col2 = new TableColumn<string,string>()
        {
            {"baz", "TEXT"}
        };
		colList.Add(col2);
		return colList;
	}

	public override string name() {
		return "mytable";
	}

		public override List<Dictionary<string, string>> generate() {
		var result =new  List<Dictionary<string, string>>();
		
		result.Add(new Dictionary<string, string> {
			{"foo", "bar"},
			{"baz", "baz"}
		});

		result.Add(new Dictionary<string, string> {
			{"foo", "bar"},
			{"baz", "baz"}
		});
				
		return result;
	}
}
```
**To test this code start an osquery shell:**
```
osqueryi --nodisable_extensions
osquery> select value from osquery_flags where name = 'extensions_socket';
```
|value
|---
|/Users/USERNAME/.osquery/shell.em

**Then start the C# extension:**
```
dotnet run -Dextension.socket=/Users/USERNAME/.osquery/shell.em MyTablePlugin.cs
```
This will register a table called "mytable". As you can see, the table will
return two rows:
```
osquery> select * from mytable;
```
| foo | baz |
|---|---|
| bar | baz |
| bar | baz |
```
osquery>
```
## Execute queries in C#
The same Thrift bindings can be used to create a C# client for the osqueryd or
osqueryi's extension socket. 
```c#
Console.WriteLine("Running C# binding for osquery...");
BasePlugin plugin = new MyTablePlugin();
PluginManager pm = PluginManager.getInstance();
pm.addPlugin(plugin);
pm.startExtension("MyTablePlugin","0.0.1","2.2.1","2.2.1");
```
