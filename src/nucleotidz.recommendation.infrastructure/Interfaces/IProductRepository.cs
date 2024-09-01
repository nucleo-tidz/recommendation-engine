using nucleotidz.recommendation.model;

namespace nucleotidz.recommendation.infrastructure.Interfaces
{
    public interface IProductRepository
    {
        Task<int> Save(IEnumerable<ProductEntity> productEntities);
        Task<int> Save(string productCode, float[] vectors);
        Task<IEnumerable<ProductEntity>> Get();
        Task<string> Get(string Email);
    }
}
