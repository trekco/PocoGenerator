#region Using

using System;
using System.Data;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

#endregion

namespace PocoGenerator
{
    public class Utility
    {
        public static string GetClassName(string name)
        {
            var s = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"));

            if (name.Contains("_") || name.Contains("-"))
            {
                var split = name.Split('_', '-');

                name = "";
                foreach (string part in split)
                {
                    name += char.ToUpper(part[0]) + part.Substring(1);
                }
            }
            name = s.Singularize(name);

            //if(name.EndsWith(string,))

            return char.ToUpper(name[0]) + name.Substring(1);
        }


        public static SqlDbType GetSqlDbType(string sqlTypeName)
        {
            object dummy = Enum.Parse(typeof(SqlDbType), sqlTypeName, true);
            return (SqlDbType) dummy;

        }
    }
}