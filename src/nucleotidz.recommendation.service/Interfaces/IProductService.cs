using nucleotidz.recommendation.model;

namespace nucleotidz.recommendation.service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<string>> Suggest(string email);
        Task<int> Create(Stream stream);
        Task<IEnumerable<ProductEntity>> Get();

    }
}
