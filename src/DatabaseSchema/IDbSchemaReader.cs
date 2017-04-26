using System.Collections.Generic;

namespace DatabaseSchema
{
    public interface IDbSchemaReader
    {
        IEnumerable<DbSchema> GetSchemas();
        DbSchema GetSchema(string tableName);
    }
}
