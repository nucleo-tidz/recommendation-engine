using MassTransit;
using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.model.Events;

namespace nucleotidz.recommendation.queue.consumer
{
    public class OrderCreatedConsumer(ITextVectorizer vectorizer, IProductVectorRepository productVectorRepository, IProductRepository productRepository) : IConsumer<ProductCreatedEvent>
    {
        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            await SaveVector(context.Message);
        }

        private async Task SaveVector(ProductCreatedEvent productCreatedEvent)
        {
            IList<ReadOnlyMemory<float>> productVector = await vectorizer.GenerateEmbeddingsAsync(new string[] { productCreatedEvent.Description });
            ReadOnlyMemory<float>[] rvector = new ReadOnlyMemory<float>[1] { productVector[0].ToArray() };
            await productVectorRepository.SaveProductVector(rvector, productCreatedEvent.Code, productCreatedEvent.Name);
            _ = await productRepository.Save(productCreatedEvent.Code, productVector[0].ToArray());
        }
    }
}
