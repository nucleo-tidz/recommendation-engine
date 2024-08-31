using MassTransit;
using nucleotidz.recommendation.model;
using nucleotidz.recommendation.service.Interfaces;

namespace nucleotidz.recommendation.service.Implementation
{
    public class ProductService(IPublishEndpoint publishEndpoint) : IProductService
    {
        public async Task<int> Create(ProductEntity product)
        {
            await publishEndpoint.Publish(product);
            return default;
        }
    }
}
