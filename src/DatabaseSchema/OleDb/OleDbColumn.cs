using System.Data.OleDb;

namespace DatabaseSchema.OleDb
{
    public class OleDbColumn : DbColumn
    {
        public OleDbType OleDbType { get; set; }
    }
}
