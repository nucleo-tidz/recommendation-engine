using nucleotidz.recommendation.model;
namespace nucleotidz.recommendation.infrastructure.Interfaces
{
    public interface IOrderRepository
    {
        public Task<int> Create(string productCode, string customerEmail);
        Task<IEnumerable<OrderEntity>> Get(string customerEmail);
        Task<int> Delete(string productCode, string customerEmail);
    }
}
