using System.Data;
using System.Data.OleDb;
using System.Linq;
using DatabaseSchema.OleDb;
using DatabaseSchema.Tests.Helpers;
using Moq;
using NUnit.Framework;

namespace DatabaseSchema.Tests.OleDb
{
    [TestFixture]
    [Parallelizable]
    public class OleDbTableColumnExtractorTests
    {
        [SetUp]
        public void SetUp()
        {
            _connection = new Mock<IOleDbConnectionWrapper>();
            _extractor = new OleDbTableColumnExtractor(_connection.Object);
        }

        private Mock<IOleDbConnectionWrapper> _connection;
        private ITableColumnExtractor _extractor;

        [Test]
        public void Extract_ShouldReturn_CollectionOf_DbColumn()
        {
            var columns = new[]
            {
                new OleDbColumn
                {
                    Name = "ID",
                    DataType = DbType.Int64,
                    OleDbType = OleDbType.BigInt,
                    IsNullable = false,
                    Position = 1
                },
                new OleDbColumn
                {
                    Name = "Username",
                    DataType = DbType.String,
                    OleDbType = OleDbType.VarWChar,
                    IsNullable = false,
                    Position = 2
                }
            };
            var schema = TableColumnSchemaHelper.Build(columns);

            _connection
                .Setup(x => x.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] {null, null, "test_1"}))
                .Returns(schema);

            var results = _extractor.Extract("test_1").ToArray();

            Assert.AreEqual(2, results.Length);

            for (var i = 0; i < columns.Length; i++)
            {
                Assert.AreEqual(columns[i].Name, results[i].Name);
                Assert.AreEqual(columns[i].DataType, results[i].DataType);
                Assert.AreEqual(columns[i].IsNullable, results[i].IsNullable);
                Assert.AreEqual(columns[i].Position, results[i].Position);
            }
        }

        [Test]
        public void Extract_WithNullableValuesSet_ShouldReturn_CollectionOf_DbColumn()
        {
            var columns = new[]
            {
                new OleDbColumn
                {
                    Name = "Price",
                    DataType = DbType.Decimal,
                    OleDbType = OleDbType.Decimal,
                    IsNullable = false,
                    Position = 1,
                    MaxLength = 10,
                    NumericPrecision = 1,
                    NumericScale = 2
                }
            };
            var schema = TableColumnSchemaHelper.Build(columns);

            _connection
                .Setup(x => x.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, "test_1" }))
                .Returns(schema);

            var results = _extractor.Extract("test_1").ToArray();

            Assert.AreEqual(1, results.Length);

            for (var i = 0; i < columns.Length; i++)
            {
                Assert.AreEqual(columns[i].Name, results[i].Name);
                Assert.AreEqual(columns[i].DataType, results[i].DataType);
                Assert.AreEqual(columns[i].IsNullable, results[i].IsNullable);
                Assert.AreEqual(columns[i].Position, results[i].Position);
                Assert.AreEqual(columns[i].MaxLength, results[i].MaxLength);
                Assert.AreEqual(columns[i].NumericPrecision, results[i].NumericPrecision);
                Assert.AreEqual(columns[i].NumericScale, results[i].NumericScale);
            }
        }

        [Test]
        public void Extract_WithRealConnection_ShouldReturn_CollectionOf_DbColumn()
        {
            using (var connection = new OleDbConnectionWrapper(TestDatabase.GetConnection()))
            {
                var extractor = new OleDbTableColumnExtractor(connection);

                connection.Open();

                var results = extractor.Extract("test_1").ToArray();

                Assert.AreEqual(7, results.Length);
            }
        }
    }
}
