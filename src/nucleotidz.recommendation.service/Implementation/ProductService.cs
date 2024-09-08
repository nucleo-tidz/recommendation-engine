using CsvHelper;
using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.model;
using nucleotidz.recommendation.model.Events;
using nucleotidz.recommendation.service.Interfaces;
using System.Globalization;
using System.Text.Json;

namespace nucleotidz.recommendation.service.Implementation
{
    public class ProductService(IEventPublisher eventPublisher,
        IProductVectorRepository productVectorRepository,
        IProductRepository productRepository) : IProductService
    {
        public async Task<IEnumerable<string>> Suggest(string email)
        {
            string lastOrder = await productRepository.Get(email);
            var data = await productVectorRepository.Search(lastOrder);

            return data.Select(x=>x.Metadata.Description.Replace("::$","--->"));
        }
        public async Task<int> Create(Stream stream)
        {
            using StreamReader reader = new(stream);
            using CsvReader csv = new(reader, CultureInfo.InvariantCulture);
            List<ProductEntity> products = csv.GetRecords<ProductEntity>().ToList();
            int records = await productRepository.Save(products);
            products.ForEach(async product =>
            await eventPublisher.Publish(
                new ProductCreatedEvent { @event = "Product Created", Code = product.Code, Description = product.Description, Name = product.Name }
                ));
            return records;
        }

        public async Task<IEnumerable<ProductEntity>> Get()
        {
            return await productRepository.Get();
        }
    }
}
