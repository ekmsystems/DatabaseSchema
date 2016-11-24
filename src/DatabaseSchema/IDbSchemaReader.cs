namespace DatabaseSchema
{
    public interface IDbSchemaReader
    {
        DbSchema GetSchema(string tableName);
    }
}
