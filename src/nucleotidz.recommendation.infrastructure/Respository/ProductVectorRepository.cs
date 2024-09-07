using MassTransit.Internals;
using Microsoft.SemanticKernel.Memory;
using nucleotidz.recommendation.infrastructure.Interfaces;

namespace nucleotidz.recommendation.infrastructure.Respository
{
    public class ProductVectorRepository(ISemanticTextMemory semanticTextMemory) : IProductVectorRepository
    {
        private readonly string ProductCollection = "Mixedprouct";
        public async Task SaveProductVector(string description, string productcode, string productName)
        {
            _ = await semanticTextMemory.SaveInformationAsync(ProductCollection, id: productcode, text: description, description: $"{productName}::${description}");
        }
        public async Task<IList<MemoryQueryResult>> Search(string description)
        {
            return await semanticTextMemory.SearchAsync(ProductCollection, description, limit: 10).ToListAsync();
        }
    }
}
