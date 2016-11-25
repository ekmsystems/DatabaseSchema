using System.Data.OleDb;

namespace DatabaseSchema.OleDb
{
    public class OleDbSchemaReader : IDbSchemaReader
    {
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
            _tableColumnExtractor = tableColumnExtractor ?? new OleDbTableColumnExtractor(connection);
            _tableIndexExtractor = tableIndexExtractor ?? new OleDbTableIndexExtractor(connection);
        }

        public DbSchema GetSchema(string tableName)
        {
            return new DbSchema
            {
                Columns = _tableColumnExtractor.Extract(tableName),
                Indexes = _tableIndexExtractor.Extract(tableName)
            };
        }
    }
}
