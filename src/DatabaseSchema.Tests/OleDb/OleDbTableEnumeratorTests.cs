using System.Data;
using System.Data.OleDb;
using System.Linq;
using DatabaseSchema.OleDb;
using Moq;
using NUnit.Framework;

namespace DatabaseSchema.Tests.OleDb
{
    [TestFixture]
    [Parallelizable]
    public class OleDbTableEnumeratorTests
    {
        [SetUp]
        public void SetUp()
        {
            _connection = new Mock<IOleDbConnectionWrapper>();
            _tableEnumerator = new OleDbTableEnumerator(_connection.Object);
        }

        private Mock<IOleDbConnectionWrapper> _connection;
        private ITableEnumerator _tableEnumerator;

        private static DataTable GetSchemaTable(params string[] tableNames)
        {
            var dt = new DataTable();

            dt.Columns.Add("TABLE_NAME");

            foreach (var tableName in tableNames)
            {
                var dr = dt.NewRow();
                dr["TABLE_NAME"] = tableName;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        [Test]
        public void GetTableNames_ShouldReturn_TableNames()
        {
            _connection
                .Setup(x => x.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] {null, null, null, "Table"}))
                .Returns(GetSchemaTable("test_1", "test_2", "test_3"));

            var results = _tableEnumerator.GetTableNames().ToArray();

            Assert.AreEqual(3, results.Length);
            Assert.AreEqual("test_1", results[0]);
            Assert.AreEqual("test_2", results[1]);
            Assert.AreEqual("test_3", results[2]);
        }
    }
}
