using System.Data.OleDb;
using System.Linq;
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
            _tableEnumerator = new Mock<ITableEnumerator>();
            _tableColumnExtractor = new Mock<ITableColumnExtractor>();
            _tableIndexExtractor = new Mock<ITableIndexExtractor>();
            _schemaReader = new OleDbSchemaReader(
                _connection.Object, 
                _tableEnumerator.Object,
                _tableColumnExtractor.Object,
                _tableIndexExtractor.Object);
        }

        private Mock<IOleDbConnectionWrapper> _connection;
        private Mock<ITableEnumerator> _tableEnumerator;
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
        public void GetSchemas_ShouldReturn_CollectionOf_DbSchema()
        {
            var tables = new[] {"table_1", "table_2"};
            var columns = new[] { new DbColumn { Name = "Column 1" } };
            var indexes = new[] { new DbIndex { Name = "idx_test" } };

            _tableEnumerator
                .Setup(x => x.GetTableNames())
                .Returns(tables);
            _tableColumnExtractor
                .Setup(x => x.Extract("test"))
                .Returns(columns);
            _tableIndexExtractor
                .Setup(x => x.Extract("test"))
                .Returns(indexes);

            var results = _schemaReader.GetSchemas().ToArray();

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual("table_1", results[0].TableName);
            Assert.AreEqual("table_2", results[1].TableName);
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

            Assert.AreEqual("test", result.TableName);
            CollectionAssert.AreEqual(columns, result.Columns);
            CollectionAssert.AreEqual(indexes, result.Indexes);
        }
    }
}
