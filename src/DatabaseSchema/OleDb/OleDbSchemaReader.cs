using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;

namespace DatabaseSchema.OleDb
{
    public class OleDbSchemaReader : IDbSchemaReader
    {
        private readonly ITableEnumerator _tableEnumerator;
        private readonly ITableColumnExtractor _tableColumnExtractor;
        private readonly ITableIndexExtractor _tableIndexExtractor;

        public OleDbSchemaReader(OleDbConnection connection, 
            ITableEnumerator tableEnumerator = null,
            ITableColumnExtractor tableColumnExtractor = null,
            ITableIndexExtractor tableIndexExtractor = null)
            : this(new OleDbConnectionWrapper(connection), tableEnumerator, tableColumnExtractor, tableIndexExtractor)
        {
        }

        internal OleDbSchemaReader(
            IOleDbConnectionWrapper connection,
            ITableEnumerator tableEnumerator,
            ITableColumnExtractor tableColumnExtractor,
            ITableIndexExtractor tableIndexExtractor)
        {
            _tableEnumerator = tableEnumerator ?? new OleDbTableEnumerator(connection);
            _tableColumnExtractor = tableColumnExtractor ?? new OleDbTableColumnExtractor(connection);
            _tableIndexExtractor = tableIndexExtractor ?? new OleDbTableIndexExtractor(connection);
        }

        public IEnumerable<DbSchema> GetSchemas()
        {
            return _tableEnumerator.GetTableNames().Select(GetSchema);
        }

        public DbSchema GetSchema(string tableName)
        {
            return new DbSchema
            {
                TableName = tableName,
                Columns = _tableColumnExtractor.Extract(tableName),
                Indexes = _tableIndexExtractor.Extract(tableName)
            };
        }
    }
}
