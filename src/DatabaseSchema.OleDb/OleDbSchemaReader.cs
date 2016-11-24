using System.Data.OleDb;

namespace DatabaseSchema.OleDb
{
    public class OleDbSchemaReader : IDbSchemaReader
    {
        private readonly IOleDbConnectionWrapper _connection;
        private readonly ITableColumnExtractor _tableColumnExtractor;
        private readonly ITableIndexExtractor _tableIndexExtractor;

        public OleDbSchemaReader(OleDbConnection connection, 
            ITableColumnExtractor tableColumnExtractor = null,
            ITableIndexExtractor tableIndexExtractor = null)
            : this(new OleDbConnectionWrapper(connection), tableColumnExtractor, tableIndexExtractor)
        {
        }

        internal OleDbSchemaReader(
            IOleDbConnectionWrapper connection,
            ITableColumnExtractor tableColumnExtractor,
            ITableIndexExtractor tableIndexExtractor)
        {
            _connection = connection;
            _tableColumnExtractor = tableColumnExtractor ?? new TableColumnExtractor();
            _tableIndexExtractor = tableIndexExtractor ?? new TableIndexExtractor();
        }

        public DbSchema GetSchema(string tableName)
        {
            return new DbSchema
            {
                Columns = _tableColumnExtractor.Extract(tableName, _connection),
                Indexes = _tableIndexExtractor.Extract(tableName, _connection)
            };
        }
    }
}
