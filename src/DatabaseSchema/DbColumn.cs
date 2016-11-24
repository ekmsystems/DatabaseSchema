using System.Data;

namespace DatabaseSchema
{
    public class DbColumn
    {
        public string Name { get; set; }
        public DbType DataType { get; set; }
        public int Position { get; set; }
        public object DefaultValue { get; set; }
        public bool IsNullable { get; set; }
        public int? MaxLength { get; set; }
        public int? NumericPrecision { get; set; }
        public int? NumericScale { get; set; }
        public int? DateTimePrecision { get; set; }
    }
}
