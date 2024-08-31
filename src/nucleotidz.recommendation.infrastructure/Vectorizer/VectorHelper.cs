using Milvus.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nucleotidz.recommendation.infrastructure.Vectorizer
{
    public class VectorHelper
    {
        private readonly MilvusClient milvusClient;
        public VectorHelper()
        {
            milvusClient = new MilvusClient("localhost");
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
