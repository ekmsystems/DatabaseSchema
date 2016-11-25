using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace DatabaseSchema.OleDb
{
    internal class OleDbTableIndexExtractor : ITableIndexExtractor
    {
        private readonly IOleDbConnectionWrapper _connection;

        public OleDbTableIndexExtractor(IOleDbConnectionWrapper connection)
        {
            _connection = connection;
        }

        public IEnumerable<DbIndex> Extract(string tableName)
        {
            var restrictions = new object[] {null, null, null, null, tableName};
            var schema = _connection.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, restrictions);
            
            foreach (DataRow dr in schema.Rows)
            {
                yield return new DbIndex
                {
                    Name = Convert.ToString(dr["INDEX_NAME"]),
                    Position = Convert.ToInt32(dr["ORDINAL_POSITION"]),
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
