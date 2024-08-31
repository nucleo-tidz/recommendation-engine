using MassTransit;
using nucleotidz.recommendation.model;

namespace nucleotidz.recommendation.queue.consumer
{
    public class OrderCreatedConsumer : IConsumer<ProductEntity>
    {
        public async  Task Consume(ConsumeContext<ProductEntity> context)
        {
            await Task.CompletedTask;
        }
    }
}
