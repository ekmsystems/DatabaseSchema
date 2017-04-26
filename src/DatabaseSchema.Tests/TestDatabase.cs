using System;
using System.Data.OleDb;
using DatabaseSchema.Tests.Helpers;

namespace DatabaseSchema.Tests
{
    internal static class TestDatabase
    {
        public static OleDbConnection GetConnection()
        {
            var provider = Environment.Is64BitProcess
                ? "Microsoft.ACE.OLEDB.12.0;"
                : "Microsoft.Jet.OLEDB.4.0";
            var filename = PathHelper.GetFullPath("./test.mdb");
            var connectionString = string.Format("Provider={0};Data Source={1};", provider, filename);

            return new OleDbConnection(connectionString);
        }
    }
}
