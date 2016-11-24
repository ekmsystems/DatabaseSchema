using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace DatabaseSchema.OleDb
{
    public interface ITableColumnExtractor
    {
        IEnumerable<DbColumn> Extract(string tableName, IOleDbConnectionWrapper connection);
    }

    internal class TableColumnExtractor : ITableColumnExtractor
    {
        public IEnumerable<DbColumn> Extract(string tableName, IOleDbConnectionWrapper connection)
        {
            var restrictions = new object[] { null, null, tableName };
            var schema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, restrictions);

            foreach (DataRow dr in schema.Rows)
            {
                yield return new DbColumn
                {
                    Name = dr.GetValue("COLUMN_NAME", Convert.ToString),
                    DataType = ParseType((OleDbType) dr.GetValue("DATA_TYPE", Convert.ToInt32)),
                    Position = dr.GetValue("ORDINAL_POSITION", Convert.ToInt32),
                    DefaultValue = dr.GetValue("COLUMN_DEFAULT"),
                    IsNullable = dr.GetValue("IS_NULLABLE", Convert.ToBoolean),
                    MaxLength = dr.GetValue<int?>("CHARACTER_MAXIMUM_LENGTH", x => Convert.ToInt32(x), null),
                    NumericPrecision = dr.GetValue<int?>("NUMERIC_PRECISION", x => Convert.ToInt32(x), null),
                    NumericScale = dr.GetValue<int?>("NUMERIC_SCALE", x => Convert.ToInt32(x), null),
                    DateTimePrecision = dr.GetValue<int?>("DATETIME_PRECISION", x => Convert.ToInt32(x), null)
                };
            }
        }

        private static DbType ParseType(OleDbType dataType)
        {
            return OleDbTypeToDbTypeConverter.Convert(dataType);
        }
    }
}
