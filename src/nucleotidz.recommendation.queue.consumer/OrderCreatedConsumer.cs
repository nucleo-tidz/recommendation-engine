using MassTransit;
using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.infrastructure.Respository;
using nucleotidz.recommendation.model;
using nucleotidz.recommendation.model.Events;
using System.Reflection.Metadata;

namespace nucleotidz.recommendation.queue.consumer
{
    public class OrderCreatedConsumer(ITextVectorizer vectorizer, IProductVectorRepository productVectorRepository) : IConsumer<ProductCreatedEvent>
    {
        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            await SaveVector(context.Message);
            await Task.CompletedTask;
        }

        private async Task<float[]> SaveVector(ProductCreatedEvent productCreatedEvent)
        {
            var productVector = await vectorizer.GenerateEmbeddingsAsync(new string[] { productCreatedEvent.Description });
            ReadOnlyMemory<float>[] rvector = new ReadOnlyMemory<float>[1] { productVector[0].ToArray() };
            await productVectorRepository.SaveProductVector(rvector, productCreatedEvent.Code, productCreatedEvent.Description);
            return productVector[0].ToArray();
        }
    }
}
