#region Using

using System.Collections.Generic;
using PocoGenerator.Generators;

#endregion

namespace PocoGenerator.Models
{
    public class TemplateData
    {
        public List<string> NamespaceImports { get; set; }
        public string Namespace { get; set; }
        public SqlTable Table { get; set; }
        public bool GenerateDapperExtentionsMapperClass { get; set; }
    }
}