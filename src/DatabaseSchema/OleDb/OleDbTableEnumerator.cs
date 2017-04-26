using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;

namespace DatabaseSchema.OleDb
{
    internal class OleDbTableEnumerator : ITableEnumerator
    {
        private readonly IOleDbConnectionWrapper _connection;

        public OleDbTableEnumerator(IOleDbConnectionWrapper connection)
        {
            _connection = connection;
        }

        public IEnumerable<string> GetTableNames()
        {
            var restrictions = new object[] {null, null, null, "Table"};
            var schema = _connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restrictions);

            return schema.Rows
                .Cast<DataRow>()
                .Select(x => Convert.ToString(x["TABLE_NAME"]));
        }
    }
}
