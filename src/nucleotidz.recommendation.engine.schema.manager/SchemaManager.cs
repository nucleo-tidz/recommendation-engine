using nucleotidz.recommendation.engine.schema.manager.Schema;
using nucleotidz.recommendation.infrastructure.Interfaces;

namespace nucleotidz.recommendation.engine.schema.manager
{
    public class SchemaManager(IVectorDatabaseHelper vectorDatabaseHelper) : ISchemaManager
    {
        public async Task CreateCollection(bool dropAndCreate)
        {
            foreach (KeyValuePair<string, Milvus.Client.CollectionSchema> schema in VectorSchema.Schemas)
            {
                bool collectionExists = await vectorDatabaseHelper.HasCollection(schema.Key);

                if (collectionExists && dropAndCreate)
                {
                    Milvus.Client.MilvusCollection existingCollection = vectorDatabaseHelper.GetCollection(schema.Key);
                    await existingCollection.DropAsync();
                }

                if (!collectionExists || dropAndCreate)
                {
                    _ = await vectorDatabaseHelper.CreateCollection(schema.Key, schema.Value);
                }
            }
        }
        public async Task CreateIndex(string collectionName, string indexName, string fieldName)
        {
            await vectorDatabaseHelper.CreateIndex(collectionName, indexName, fieldName);
        }
    }
}
