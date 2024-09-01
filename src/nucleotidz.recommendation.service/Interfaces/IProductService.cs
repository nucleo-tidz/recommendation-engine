using nucleotidz.recommendation.model;

namespace nucleotidz.recommendation.service.Interfaces
{
    public interface IProductService
    {
        Task Suggest(string email);
        Task<int> Create(Stream stream);
        Task<IEnumerable<ProductEntity>> Get();

    }
}
