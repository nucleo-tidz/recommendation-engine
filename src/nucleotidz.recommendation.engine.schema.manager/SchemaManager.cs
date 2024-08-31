using nucleotidz.recommendation.infrastructure.Interfaces;

namespace nucleotidz.recommendation.engine.schema.manager
{
    public class SchemaManager(IVectorDatabaseHelper vectorDatabaseHelper) : ISchemaManager
    {
        public async Task CreateCollection(bool dropAndCreate)
        {
            foreach (var schema in VectorSchema.Schemas)
            {
                var collectionExists = await vectorDatabaseHelper.HasCollection(schema.Key);

                if (collectionExists && dropAndCreate)
                {
                    var existingCollection = vectorDatabaseHelper.GetCollection(schema.Key);
                    await existingCollection.DropAsync();
                }

                if (!collectionExists || dropAndCreate)
                {
                    await vectorDatabaseHelper.CreateCollection(schema.Key, schema.Value);
                }
            }
        }
        public async Task CreateIndex(string collectionName, string indexName, string fieldName)
        {
            await vectorDatabaseHelper.CreateIndex(collectionName, indexName, fieldName);
        }
    }
}
