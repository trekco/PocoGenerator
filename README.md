# PocoGenerator

PocoGenerator generates POCO(Plain old CLR objects) classes from a database

----------
## Features ##
1. Generates POCO classes from MSSQL database
2. Support Dapper Extensions

## Requirements ##
1. .net 4.5.2(can be change to any other version)
2. Visual Studio 2013
3. A database(MSSQL)

## Build ##
Build by running build.bat


## Usage ##
Once the config is updated for your environment, all you have to do is run PocoGenerator.exe

To configure the application edit the config file "PocoGenerator.exe.config"

**AppSettings**

      <appSettings>
	    <add key="BaseClass" value=""/>
	    <add key="DatabaseName" value="AdventureWorks2014"/>
	    <add key="Using" value="System"/>
	    <add key="Namespace" value="MyProject.Objects"/>
	    <add key="OutputPath" value="c:\temp\generated"/>
	    <add key="OutputFileExtention" value="{0}.generated.cs"/>
	    <add key="GenerateDapperExtentionsMapperClass" value="true"/>
      </appSettings>
- BaseClass 
	- The class that all POCO classes should inherit from.
- DatabaseName
	- The name of your database.
- Using
	- Usings that should be inserted. Semicolon ";" separated list.
- Namespace
	- The Namespace of all the POCO classes.
- OutputPath
	- Where your files should be saved too.
- OutputFileExtention
	- extention/file name format.
- GenerateDapperExtentionsMapperClass
	- Set to true if you want to generate Dapper Extensions mapper classes

**ConnectionStrings**

    <connectionStrings>
    	<add name="dbConnection" ConnectionString="server=localhost;user=sa;password=password;database=AdventureWorks2014"/>
    </connectionStrings>

Make sure that the connection string "dbConnection" points to your database

----------

## Contribute ##

Feel free to contribute. This app still needs a lot of improvements. So far it is still basic

## Future Features ##

1. MySQL support
2. Proper error handling
3. Detailed console output