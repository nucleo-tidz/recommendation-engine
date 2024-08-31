using MassTransit;
using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.infrastructure.Respository;
using nucleotidz.recommendation.model;
using nucleotidz.recommendation.model.Events;
using nucleotidz.recommendation.service.Interfaces;

namespace nucleotidz.recommendation.service.Implementation
{
    public class ProductService(IEventPublisher eventPublisher,ITextVectorizer vectorizer,IProductVectorRepository productVectorRepository) : IProductService
    {
        public async Task<int> Create(ProductEntity product)
        {
            await eventPublisher.Publish(new ProductCreatedEvent { @event = "Product Created", Code = product.Code, Description = product.Description ,Name=product.Name});
            return default;
        }
        public async Task Search(string description)
        {
            var productVector = await vectorizer.GenerateEmbeddingsAsync(new string[] { description });
            ReadOnlyMemory<float>[] rvector = new ReadOnlyMemory<float>[1] { productVector[0].ToArray() };
            await productVectorRepository.Search(rvector);
        }
    }
}
