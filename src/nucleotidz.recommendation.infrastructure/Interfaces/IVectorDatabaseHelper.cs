using Milvus.Client;

namespace nucleotidz.recommendation.infrastructure.Interfaces
{
    public interface IVectorDatabaseHelper
    {
        Task<bool> HasCollection(string collectionName);
        Task<MilvusCollection> CreateCollection(string collectionName, CollectionSchema schema);
        MilvusCollection GetCollection(string collectionName);

    }
}
