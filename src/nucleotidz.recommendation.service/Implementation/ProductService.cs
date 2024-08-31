using MassTransit;
using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.model;
using nucleotidz.recommendation.model.Events;
using nucleotidz.recommendation.service.Interfaces;

namespace nucleotidz.recommendation.service.Implementation
{
    public class ProductService(IEventPublisher eventPublisher) : IProductService
    {
        public async Task<int> Create(ProductEntity product)
        {
            await eventPublisher.Publish(new ProductCreatedEvent { @event = "Product Created", Code = product.Code, Description = product.Description });
            return default;
        }
    }
}
