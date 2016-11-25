using System.Data.OleDb;
using DatabaseSchema.OleDb;
using Moq;
using NUnit.Framework;

namespace DatabaseSchema.Tests.OleDb
{
    [TestFixture]
    [Parallelizable]
    public class OleDbSchemaReaderTests
    {
        [SetUp]
        public void SetUp()
        {
            _connection = new Mock<IOleDbConnectionWrapper>();
            _tableColumnExtractor = new Mock<ITableColumnExtractor>();
            _tableIndexExtractor = new Mock<ITableIndexExtractor>();
            _schemaReader = new OleDbSchemaReader(
                _connection.Object, 
                _tableColumnExtractor.Object,
                _tableIndexExtractor.Object);
        }

        private Mock<IOleDbConnectionWrapper> _connection;
        private Mock<ITableColumnExtractor> _tableColumnExtractor;
        private Mock<ITableIndexExtractor> _tableIndexExtractor;
        private IDbSchemaReader _schemaReader;

        [Test]
        public void Constructor_ShouldNot_ThrowException()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.DoesNotThrow(() => new OleDbSchemaReader(new OleDbConnection()));
        }

        [Test]
        public void GetSchema_ShouldReturn_DbSchema()
        {
            var columns = new[] {new DbColumn {Name = "Column 1"}};
            var indexes = new[] {new DbIndex {Name = "idx_test"}};

            _tableColumnExtractor
                .Setup(x => x.Extract("test"))
                .Returns(columns);
            _tableIndexExtractor
                .Setup(x => x.Extract("test"))
                .Returns(indexes);

            var result = _schemaReader.GetSchema("test");

            CollectionAssert.AreEqual(columns, result.Columns);
            CollectionAssert.AreEqual(indexes, result.Indexes);
        }
    }
}
