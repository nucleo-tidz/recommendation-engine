using CsvHelper;
using MassTransit;
using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.infrastructure.Respository;
using nucleotidz.recommendation.model;
using nucleotidz.recommendation.model.Events;
using nucleotidz.recommendation.service.Interfaces;
using System.Globalization;

namespace nucleotidz.recommendation.service.Implementation
{
    public class ProductService(IEventPublisher eventPublisher, ITextVectorizer vectorizer, IProductVectorRepository productVectorRepository, IProductRepository productRepository) : IProductService
    {
        public async Task<int> Create(ProductEntity product)
        {
            await eventPublisher.Publish(new ProductCreatedEvent { @event = "Product Created", Code = product.Code, Description = product.Description, Name = product.Name });
            return default;
        }
        public async Task Search(string description)
        {
            var productVector = await vectorizer.GenerateEmbeddingsAsync(new string[] { description });
            ReadOnlyMemory<float>[] rvector = new ReadOnlyMemory<float>[1] { productVector[0].ToArray() };
            await productVectorRepository.Search(rvector);
        }
        public async Task<int> Create(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                List<ProductEntity> products = csv.GetRecords<ProductEntity>().ToList();
              return  await  productRepository.Save(products);
            }
        }
    }
}
