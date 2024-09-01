namespace nucleotidz.recommendation.engine.schema.manager
{
    public interface ISchemaManager
    {
        Task CreateCollection(bool dropAndCreate);
        Task CreateIndex(string collectionName, string indexName, string fieldName);
    }
}
