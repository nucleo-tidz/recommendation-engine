using MassTransit;
using nucleotidz.recommendation.model;
using nucleotidz.recommendation.model.Events;

namespace nucleotidz.recommendation.queue.consumer
{
    public class OrderCreatedConsumer : IConsumer<ProductCreatedEvent>
    {
        public async  Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            await Task.CompletedTask;
        }
    }
}
