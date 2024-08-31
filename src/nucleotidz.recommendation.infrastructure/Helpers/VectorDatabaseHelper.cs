using Milvus.Client;
using nucleotidz.recommendation.infrastructure.Interfaces;

namespace nucleotidz.recommendation.infrastructure.Helpers
{
    public class VectorDatabaseHelper: IVectorDatabaseHelper
    {
        private readonly MilvusClient milvusClient;
        public VectorDatabaseHelper()
        {
            milvusClient = new MilvusClient("standalone");
        }
        public async Task<bool> HasCollection(string collectionName)
        {
            var k = await milvusClient.HealthAsync();
            return await milvusClient.HasCollectionAsync(collectionName);
        }
        public async Task<MilvusCollection> CreateCollection(string collectionName, CollectionSchema schema)
        {
            return await milvusClient.CreateCollectionAsync(collectionName, schema);
        }
        public MilvusCollection GetCollection(string collectionName)
        {
            return milvusClient.GetCollection(collectionName);
        }
    }
}
