using System.Collections.Generic;

namespace PocoGenerator.Generators
{
    public class SqlTable
    {
        public SqlTable()
        {
            Fields = new List<SqlField>();
        }

        public string Name { get; set; }
        public string ClassName { get; set; }
        public List<SqlField> Fields { get; set; }
    }
}
