using System.Collections.Generic;
using System.Data;
using DatabaseSchema.OleDb;

namespace DatabaseSchema.Tests.Helpers
{
    internal static class TableColumnSchemaHelper
    {
        public static DataTable Build(IEnumerable<OleDbColumn> columns)
        {
            var dt = BuildSchemaTable();

            foreach (var column in columns)
            {
                var dr = dt.NewRow();

                dr["COLUMN_NAME"] = column.Name;
                dr["DATA_TYPE"] = (int) column.OleDbType;
                dr["ORDINAL_POSITION"] = column.Position;
                dr["COLUMN_DEFAULT"] = column.DefaultValue;
                dr["IS_NULLABLE"] = column.IsNullable;
                dr["CHARACTER_MAXIMUM_LENGTH"] = column.MaxLength;
                dr["NUMERIC_PRECISION"] = column.NumericPrecision;
                dr["NUMERIC_SCALE"] = column.NumericScale;

                dt.Rows.Add(dr);
            }

            return dt;
        }

        private static DataTable BuildSchemaTable()
        {
            var dt = new DataTable();

            dt.Columns.AddRange(new[]
            {
                new DataColumn("COLUMN_NAME"),
                new DataColumn("DATA_TYPE"),
                new DataColumn("ORDINAL_POSITION"),
                new DataColumn("COLUMN_DEFAULT"),
                new DataColumn("IS_NULLABLE"),
                new DataColumn("CHARACTER_MAXIMUM_LENGTH"),
                new DataColumn("NUMERIC_PRECISION"),
                new DataColumn("NUMERIC_SCALE")
            });

            return dt;
        }
    }
}
