using System.Data;
using System.Data.OleDb;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace DatabaseSchema.OleDb.Tests
{
    [TestFixture]
    [Parallelizable]
    public class TableIndexExtractorTests
    {
        [SetUp]
        public void SetUp()
        {
            _extractor = new TableIndexExtractor();
        }

        private ITableIndexExtractor _extractor;

        [Test]
        public void Extract_ShouldReturn_CollectionOf_DbIndex()
        {
            var connection = new Mock<IOleDbConnectionWrapper>();

            connection
                .Setup(x => x.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, new object[] {}))
                .Returns(new DataTable());

            var results = _extractor.Extract("test_1", connection.Object);

            Assert.IsNotEmpty(results);
        }

        [Test]
        public void Extract_WithRealConnection_ShouldReturn_CollectionOf_DbIndex()
        {
            using (var connection = new OleDbConnectionWrapper(TestDatabase.GetConnection()))
            {
                connection.Open();

                var results = _extractor.Extract("test_1", connection).ToArray();

                Assert.AreEqual(2, results.Length);
            }
        }
    }
}