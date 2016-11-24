namespace DatabaseSchema
{
    public class DbIndex
    {
        public string Name { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsUnique { get; set; }
        public bool IsClustered { get; set; }
        public bool IsNullable { get; set; }
        public string ColumnName { get; set; }
    }
}
