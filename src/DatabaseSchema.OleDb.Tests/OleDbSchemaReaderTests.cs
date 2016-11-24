using Moq;
using NUnit.Framework;

namespace DatabaseSchema.OleDb.Tests
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
        private OleDbSchemaReader _schemaReader;

        [Test]
        public void GetSchema_ShouldReturn_DbSchema()
        {
            var columns = new[] {new DbColumn {Name = "Column 1"}};
            var indexes = new[] {new DbIndex {Name = "idx_test"}};

            _tableColumnExtractor
                .Setup(x => x.Extract("test", _connection.Object))
                .Returns(columns);
            _tableIndexExtractor
                .Setup(x => x.Extract("test", _connection.Object))
                .Returns(indexes);

            var result = _schemaReader.GetSchema("test");

            CollectionAssert.AreEqual(columns, result.Columns);
            CollectionAssert.AreEqual(indexes, result.Indexes);
        }
    }
}
