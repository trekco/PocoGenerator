namespace PocoGenerator.Generators
{
    public class SqlField
    {
        public string DbType { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public bool IsPrimaryKey { get; set; }
    }
}
