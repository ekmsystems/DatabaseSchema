using System.Collections.Generic;

namespace DatabaseSchema
{
    public interface ITableColumnExtractor
    {
        IEnumerable<DbColumn> Extract(string tableName);
    }
}