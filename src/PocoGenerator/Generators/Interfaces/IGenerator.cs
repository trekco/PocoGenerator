using System.Collections.Generic;
using PocoGenerator.Models;

namespace PocoGenerator.Generators.Interfaces
{
    public interface IGenerator
    {
        void LoadOptions(GenerationOptions options);
        void Generate();
        List<SqlTable> GetDatabaseTables();
        void PopulateTableFields(List<SqlTable> tables);
    }
}
