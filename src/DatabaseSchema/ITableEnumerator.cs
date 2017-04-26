using System.Collections.Generic;

namespace DatabaseSchema
{
    public interface ITableEnumerator
    {
        IEnumerable<string> GetTableNames();
    }
}
