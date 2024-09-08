using nucleotidz.recommendation.model;
namespace nucleotidz.recommendation.service.Interfaces
{
    public interface IOrderService
    {
        Task<int> Create(string productCode, string customerEmail);
        Task<IEnumerable<OrderEntity>> Get(string customerEmail);
        Task<int> Delete(string productCode, string customerEmail);
    }
}
