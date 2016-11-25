using System;
using System.Data;
using System.Data.OleDb;

namespace DatabaseSchema.OleDb
{
    public interface IOleDbConnectionWrapper : IDisposable
    {
        void Open();
        DataTable GetOleDbSchemaTable(Guid schema, object[] restrictions);
    }

    internal class OleDbConnectionWrapper : IOleDbConnectionWrapper
    {
        private readonly OleDbConnection _connection;

        public OleDbConnectionWrapper(OleDbConnection connection)
        {
            _connection = connection;
        }

        public void Open()
        {
            _connection.Open();
        }

        public DataTable GetOleDbSchemaTable(Guid schema, object[] restrictions)
        {
            return _connection.GetOleDbSchemaTable(schema, restrictions);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
