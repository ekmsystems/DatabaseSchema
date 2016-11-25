using System.Data;
using System.Data.OleDb;

namespace DatabaseSchema.OleDb
{
    internal static class OleDbTypeToDbTypeConverter
    {
        public static DbType Convert(OleDbType dataType)
        {
            switch (dataType)
            {
                case OleDbType.BSTR:
                case OleDbType.LongVarChar:
                case OleDbType.LongVarWChar:
                case OleDbType.VarChar:
                case OleDbType.VarWChar:
                case OleDbType.WChar:
                    return DbType.String;
                case OleDbType.BigInt:
                    return DbType.Int64;
                case OleDbType.Binary:
                case OleDbType.LongVarBinary:
                case OleDbType.VarBinary:
                    return DbType.Binary;
                case OleDbType.Boolean:
                    return DbType.Boolean;
                case OleDbType.Char:
                    return DbType.Byte;
                case OleDbType.Currency:
                    return DbType.Currency;
                case OleDbType.DBDate:
                    return DbType.Date;
                case OleDbType.DBTime:
                    return DbType.Time;
                case OleDbType.DBTimeStamp:
                    return DbType.DateTimeOffset;
                case OleDbType.Date:
                    return DbType.Date;
                case OleDbType.Decimal:
                case OleDbType.Numeric:
                case OleDbType.VarNumeric:
                    return DbType.Decimal;
                case OleDbType.Double:
                    return DbType.Double;
                case OleDbType.Filetime:
                    return DbType.DateTime;
                case OleDbType.Guid:
                    return DbType.Guid;
                case OleDbType.Integer:
                    return DbType.Int32;
                case OleDbType.Single:
                    return DbType.Single;
                case OleDbType.SmallInt:
                    return DbType.Int16;
                case OleDbType.TinyInt:
                    return DbType.SByte;
                case OleDbType.UnsignedBigInt:
                    return DbType.UInt64;
                case OleDbType.UnsignedInt:
                    return DbType.UInt32;
                case OleDbType.UnsignedSmallInt:
                    return DbType.UInt16;
                case OleDbType.UnsignedTinyInt:
                    return DbType.Byte;
                default:
                    return DbType.Object;
            }
        }
    }
}
