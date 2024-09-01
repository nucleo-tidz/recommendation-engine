using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.service.Interfaces;

namespace nucleotidz.recommendation.service.Implementation
{
    internal class OrderService(IOrderRepository orderRepository) : IOrderService
    {
        public async Task<int> Create(string productCode, string customerEmail)
        {
            return await orderRepository.Create(productCode, customerEmail);
        }
    }
}
