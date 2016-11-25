using System.Data;
using System.Data.OleDb;
using DatabaseSchema.OleDb;
using NUnit.Framework;

namespace DatabaseSchema.Tests.OleDb
{
    [TestFixture]
    [Parallelizable]
    public class OleDbTypeToDbTypeConverterTests
    {
        [Test]
        [TestCase(OleDbType.BSTR, DbType.String)]
        [TestCase(OleDbType.LongVarChar, DbType.String)]
        [TestCase(OleDbType.LongVarWChar, DbType.String)]
        [TestCase(OleDbType.VarChar, DbType.String)]
        [TestCase(OleDbType.VarWChar, DbType.String)]
        [TestCase(OleDbType.WChar, DbType.String)]
        [TestCase(OleDbType.BigInt, DbType.Int64)]
        [TestCase(OleDbType.Binary, DbType.Binary)]
        [TestCase(OleDbType.LongVarBinary, DbType.Binary)]
        [TestCase(OleDbType.VarBinary, DbType.Binary)]
        [TestCase(OleDbType.Boolean, DbType.Boolean)]
        [TestCase(OleDbType.Char, DbType.Byte)]
        [TestCase(OleDbType.Currency, DbType.Currency)]
        [TestCase(OleDbType.DBDate, DbType.Date)]
        [TestCase(OleDbType.DBTime, DbType.Time)]
        [TestCase(OleDbType.DBTimeStamp, DbType.DateTimeOffset)]
        [TestCase(OleDbType.Date, DbType.Date)]
        [TestCase(OleDbType.Decimal, DbType.Decimal)]
        [TestCase(OleDbType.Numeric, DbType.Decimal)]
        [TestCase(OleDbType.VarNumeric, DbType.Decimal)]
        [TestCase(OleDbType.Double, DbType.Double)]
        [TestCase(OleDbType.Filetime, DbType.DateTime)]
        [TestCase(OleDbType.Guid, DbType.Guid)]
        [TestCase(OleDbType.Integer, DbType.Int32)]
        [TestCase(OleDbType.Single, DbType.Single)]
        [TestCase(OleDbType.SmallInt, DbType.Int16)]
        [TestCase(OleDbType.TinyInt, DbType.SByte)]
        [TestCase(OleDbType.UnsignedBigInt, DbType.UInt64)]
        [TestCase(OleDbType.UnsignedInt, DbType.UInt32)]
        [TestCase(OleDbType.UnsignedSmallInt, DbType.UInt16)]
        [TestCase(OleDbType.UnsignedTinyInt, DbType.Byte)]
        [TestCase(OleDbType.Empty, DbType.Object)]
        [TestCase(OleDbType.Error, DbType.Object)]
        public void Convert_ShouldReturn_DbType(OleDbType oleDbType, DbType expected)
        {
            var result = OleDbTypeToDbTypeConverter.Convert(oleDbType);

            Assert.AreEqual(expected, result);
        }
    }
}
