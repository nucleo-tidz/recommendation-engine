using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.service.Interfaces;
using nucleotidz.recommendation.model;
namespace nucleotidz.recommendation.service.Implementation
{
    internal class OrderService(IOrderRepository orderRepository) : IOrderService
    {
        public async Task<int> Create(string productCode, string customerEmail)
        {
            return await orderRepository.Create(productCode, customerEmail);
        }
        public async Task<IEnumerable<OrderEntity>> Get(string customerEmail)
        {
            return await orderRepository.Get(customerEmail);
        }
        public async Task<int> Delete(string productCode, string customerEmail)
        {
            return await orderRepository.Delete(productCode, customerEmail);
        }
    }
}
