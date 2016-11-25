using System.Data.OleDb;
using System.Linq;
using DatabaseSchema.OleDb;
using DatabaseSchema.OleDb.Tests;
using DatabaseSchema.Tests.Helpers;
using Moq;
using NUnit.Framework;

namespace DatabaseSchema.Tests.OleDb
{
    [TestFixture]
    [Parallelizable]
    public class OleDbTableIndexExtractorTests
    {
        [SetUp]
        public void SetUp()
        {
            _connection = new Mock<IOleDbConnectionWrapper>();
            _extractor = new OleDbTableIndexExtractor(_connection.Object);
        }

        private Mock<IOleDbConnectionWrapper> _connection;
        private ITableIndexExtractor _extractor;

        [Test]
        public void Extract_ShouldReturn_CollectionOf_DbIndex()
        {
            var indexes = new[]
            {
                new DbIndex
                {
                    Name = "idx_ID",
                    IsPrimaryKey = true,
                    IsUnique = true,
                    IsClustered = true,
                    IsNullable = false,
                    ColumnName = "ID"
                },
                new DbIndex
                {
                    Name = "idx_Username",
                    IsPrimaryKey = false,
                    IsUnique = false,
                    IsClustered = false,
                    IsNullable = true,
                    ColumnName = "Username"
                }
            };
            var schema = TableIndexSchemaHelper.Build(indexes);

            _connection
                .Setup(x => x.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, new object[]
                {
                    null, null, null, null, "test_1"
                }))
                .Returns(schema);

            var results = _extractor.Extract("test_1").ToArray();

            Assert.AreEqual(2, results.Length);

            for (var i = 0; i < indexes.Length; i++)
            {
                Assert.AreEqual(indexes[i].Name, results[i].Name);
                Assert.AreEqual(indexes[i].IsPrimaryKey, results[i].IsPrimaryKey);
                Assert.AreEqual(indexes[i].IsUnique, results[i].IsUnique);
                Assert.AreEqual(indexes[i].IsClustered, results[i].IsClustered);
                Assert.AreEqual(indexes[i].IsNullable, results[i].IsNullable);
                Assert.AreEqual(indexes[i].ColumnName, results[i].ColumnName);
            }
        }

        [Test]
        public void Extract_WithRealConnection_ShouldReturn_CollectionOf_DbIndex()
        {
            using (var connection = new OleDbConnectionWrapper(TestDatabase.GetConnection()))
            {
                var extractor = new OleDbTableIndexExtractor(connection);

                connection.Open();

                var results = extractor.Extract("test_1").ToArray();

                Assert.AreEqual(2, results.Length);
            }
        }
    }
}