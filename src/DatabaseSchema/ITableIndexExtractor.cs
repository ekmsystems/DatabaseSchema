using System.Collections.Generic;

namespace DatabaseSchema
{
    public interface ITableIndexExtractor
    {
        IEnumerable<DbIndex> Extract(string tableName);
    }
}