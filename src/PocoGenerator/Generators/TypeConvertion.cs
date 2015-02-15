#region Using

using System.Collections.Generic;

#endregion

namespace PocoGenerator.Generators
{
    public class MsSqlTypeConvertion
    {
        private readonly Dictionary<string, string> _convertionDictionary;

        public MsSqlTypeConvertion()
        {
            _convertionDictionary = new Dictionary<string, string>();

            _convertionDictionary.Add("bigint", "Int64");
            _convertionDictionary.Add("binary", "byte[]");
            _convertionDictionary.Add("bit", "bool");
            _convertionDictionary.Add("char", "string");
            _convertionDictionary.Add("date", "DateTime");
            _convertionDictionary.Add("datetime", "DateTime");
            _convertionDictionary.Add("datetime2", "DateTime");
            _convertionDictionary.Add("datetimeoffset", "DateTimeOffset");
            _convertionDictionary.Add("decimal", "decimal");
            _convertionDictionary.Add("float", "double");
            _convertionDictionary.Add("image", "byte[]");
            _convertionDictionary.Add("int", "int");
            _convertionDictionary.Add("money", "decimal");
            _convertionDictionary.Add("nchar", "string");
            _convertionDictionary.Add("ntext", "string");
            _convertionDictionary.Add("numeric", "decimal");
            _convertionDictionary.Add("nvarchar", "string");
            _convertionDictionary.Add("real", "Single");
            _convertionDictionary.Add("rowversion", "byte[]");
            _convertionDictionary.Add("smallint", "int");
            _convertionDictionary.Add("smallmoney", "decimal");
            _convertionDictionary.Add("sql_variant", "object");
            _convertionDictionary.Add("text", "string");
            _convertionDictionary.Add("time", "TimeSpan");
            _convertionDictionary.Add("tinyint", "byte");
            _convertionDictionary.Add("uniqueidentifier", "Guid");
            _convertionDictionary.Add("varbinary", "byte[]");
            _convertionDictionary.Add("varchar", "string");
            _convertionDictionary.Add("xml", "string");
            _convertionDictionary.Add("flag", "bool");
            _convertionDictionary.Add("hierarchyid", "string");
            _convertionDictionary.Add("sysname", "string");

        }

        public string GetType(string dbType)
        {
            dbType = dbType.ToLower();

            if (_convertionDictionary.ContainsKey(dbType))
                return _convertionDictionary[dbType];


            foreach (KeyValuePair<string, string> keyValuePair in _convertionDictionary)
            {
                if (dbType.Contains(keyValuePair.Key))
                    return keyValuePair.Value;
            }

            return "object";
        }
    }
}