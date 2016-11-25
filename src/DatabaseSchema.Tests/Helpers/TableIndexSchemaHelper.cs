using System.Collections.Generic;
using System.Data;

namespace DatabaseSchema.Tests.Helpers
{
    internal static class TableIndexSchemaHelper
    {
        public static DataTable Build(IEnumerable<DbIndex> indexes)
        {
            var dt = BuildSchemaTable();

            foreach (var index in indexes)
            {
                var dr = dt.NewRow();

                dr["INDEX_NAME"] = index.Name;
                dr["ORDINAL_POSITION"] = index.Position;
                dr["PRIMARY_KEY"] = index.IsPrimaryKey;
                dr["UNIQUE"] = index.IsUnique;
                dr["CLUSTERED"] = index.IsClustered;
                dr["NULLS"] = index.IsNullable;
                dr["COLUMN_NAME"] = index.ColumnName;

                dt.Rows.Add(dr);
            }

            return dt;
        }

        private static DataTable BuildSchemaTable()
        {
            var dt = new DataTable();

            dt.Columns.AddRange(new[]
            {
                new DataColumn("INDEX_NAME"),
                new DataColumn("ORDINAL_POSITION"), 
                new DataColumn("PRIMARY_KEY"),
                new DataColumn("UNIQUE"),
                new DataColumn("CLUSTERED"),
                new DataColumn("NULLS"),
                new DataColumn("COLUMN_NAME")
            });

            return dt;
        }
    }
}
