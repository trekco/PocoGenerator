using System.Collections.Generic;

namespace PocoGenerator.Models
{
    public class GenerationOptions
    {
        public string OutputPath { get; set; }
        public string ConnectionString { get; set; }
        public string OutputFileExtention { get; set; }
        public List<string> Using { get; set; }
        public string BaseClass { get; set; }
        public string DatabaseName { get; set; }
        public string OutputFileTemplate { get; set; }
        public string Namespace { get; set; }
        public bool GenerateDapperExtentionsMapperClass { get; set; }
    }
}
