using System;
using System.Data;

namespace DatabaseSchema
{
    internal static class DataRowExtensions
    {
        public static object GetValue(this DataRow dr, string columnName, object defaultValue = null)
        {
            return Convert.IsDBNull(dr[columnName]) ? defaultValue : dr[columnName];
        }

        public static T GetValue<T>(this DataRow dr, string columnName, 
            Func<object, T> converter,
            T defaultValue = default(T))
        {
            return Convert.IsDBNull(dr[columnName]) ? defaultValue : converter(dr[columnName]);
        }
    }
}
