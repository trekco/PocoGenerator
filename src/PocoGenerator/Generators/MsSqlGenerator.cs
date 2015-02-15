#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using PocoGenerator.Generators.Interfaces;
using PocoGenerator.Models;
using RazorEngine;
using RazorEngine.Templating;

#endregion

namespace PocoGenerator.Generators
{
    public class MsSqlGenerator : IGenerator
    {
        private GenerationOptions _options;
        private MsSqlTypeConvertion _typeConverter = new MsSqlTypeConvertion();

        public void LoadOptions(GenerationOptions options)
        {
            _options = options;

            if (!Directory.Exists(_options.OutputPath))
                Directory.CreateDirectory(_options.OutputPath);
        }

        public void Generate()
        {
            var tables = GetDatabaseTables();
            PopulateTableFields(tables);

            GenerateFiles(tables);
        }

        public List<SqlTable> GetDatabaseTables()
        {
            var result = new List<SqlTable>();
            var data = GetDbTablesFromSql();

            if (data != null && data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    SqlTable table = new SqlTable
                    {
                        Name = Convert.ToString(row["TABLE_NAME"]),
                        ClassName = Utility.GetClassName(Convert.ToString(row["TABLE_NAME"]))
                    };

                    result.Add(table);
                }
            }

            return result;
        }

        public void PopulateTableFields(List<SqlTable> tables)
        {
            var query = @"SELECT 
                            c.name 'Column Name',
                            t.Name 'Data type',
	                        isc.CHARACTER_MAXIMUM_LENGTH 'Max Length',
                            c.precision ,
                            c.scale ,
                            c.is_nullable,
                            ISNULL(i.is_primary_key, 0) 'Primary Key'
                        FROM    
                            sys.columns c
	                        join 
	                        information_schema.columns isc on isc.COLUMN_NAME = c.name and isc.table_name = '{0}'
                        INNER JOIN 
                            sys.types t ON c.user_type_id = t.user_type_id
                        LEFT OUTER JOIN 
                            sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id
                        LEFT OUTER JOIN 
                            sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id
                        WHERE
                            c.object_id = OBJECT_ID(isc.TABLE_SCHEMA + '.{0}')";

            foreach (SqlTable table in tables)
            {
                var fieldData = ExecuteSelectQuery(string.Format(query, table.Name));

                if (fieldData != null)
                {
                    foreach (DataRow row in fieldData.Rows)
                    {
                        int length;

                        var field = new SqlField();

                        field.Name = (string) row["Column Name"];
                        field.DbType = (string) row["Data type"];
                        field.Type = _typeConverter.GetType(field.DbType);
                        field.IsPrimaryKey = (bool) row["Primary Key"];
                        field.Length = int.TryParse(Convert.ToString(row["Max Length"]), out length) ? length : 0;

                        table.Fields.Add(field);
                    }
                }
            }
        }

        private void GenerateFiles(List<SqlTable> tables)
        {
            string template = GetTemplate();

            foreach (SqlTable table in tables)
            {
                var model = new TemplateData();

                model.Namespace = _options.Namespace;
                model.Table = table;
                model.NamespaceImports = new List<string>();
                model.NamespaceImports.AddRange(_options.Using);
                model.GenerateDapperExtentionsMapperClass = _options.GenerateDapperExtentionsMapperClass;


                var result = Engine.Razor.RunCompile(new LoadedTemplateSource(template), "PocoGenerator.Templates.PocoTemplate.cshtml", typeof (TemplateData), model);

                var fileName = string.Format(_options.OutputFileExtention, table.Name);
                var savePath = string.Format("{0}\\{1}", _options.OutputPath.TrimEnd('\\'), fileName);

                File.WriteAllText(savePath, result);

            }
        }

        
        private string GetTemplate()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "PocoGenerator.Templates.PocoTemplate.cshtml";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private DataTable GetDbTablesFromSql()
        {
            string query = string.Format("SELECT * FROM information_schema.tables WHERE TABLE_CATALOG = '{0}' AND TABLE_TYPE = 'BASE TABLE'", _options.DatabaseName);

            var tables = ExecuteSelectQuery(query);
            return tables;
        }

        private DataTable ExecuteSelectQuery(string Query)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(Query, connection))
                {
                    SqlDataReader dr = cmd.ExecuteReader();

                    data.Load(dr);
                    dr.Close();
                }
            }

            return data;
        }
    }
}