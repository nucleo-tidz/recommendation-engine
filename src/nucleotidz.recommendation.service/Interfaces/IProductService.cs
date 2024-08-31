using nucleotidz.recommendation.model;

namespace nucleotidz.recommendation.service.Interfaces
{
    public interface IProductService
    {
        Task<int> Create(ProductEntity product);
        Task Search(string description);

    }
}
