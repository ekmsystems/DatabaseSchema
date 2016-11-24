using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DatabaseSchema.OleDb
{
    public interface ITableIndexExtractor
    {
        IEnumerable<DbIndex> Extract(string tableName, IOleDbConnectionWrapper connection);
    }

    internal class TableIndexExtractor : ITableIndexExtractor
    {
        public IEnumerable<DbIndex> Extract(string tableName, IOleDbConnectionWrapper connection)
        {
            var restrictions = new object[] {null, null, null, null, tableName};
            var schema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, restrictions);
            
            foreach (DataRow dr in schema.Rows)
            {
                yield return new DbIndex
                {
                    Name = Convert.ToString(dr["INDEX_NAME"]),
                    IsPrimaryKey = Convert.ToBoolean(dr["PRIMARY_KEY"]),
                    IsUnique = Convert.ToBoolean(dr["UNIQUE"]),
                    IsClustered = Convert.ToBoolean(dr["CLUSTERED"]),
                    IsNullable = Convert.ToBoolean(dr["NULLS"]),
                    ColumnName = Convert.ToString(dr["COLUMN_NAME"])
                };
            }
        }
    }
}
