using MassTransit;
using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.model.Events;

namespace nucleotidz.recommendation.queue.consumer
{
    public class OrderCreatedConsumer(IProductVectorRepository productVectorRepository) : IConsumer<ProductCreatedEvent>
    {
        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            await SaveVector(context.Message);
        }

        private async Task SaveVector(ProductCreatedEvent productCreatedEvent)
        {
            await productVectorRepository.SaveProductVector(productCreatedEvent.Description, productCreatedEvent.Code,productCreatedEvent.Name);
        }
    }
}
