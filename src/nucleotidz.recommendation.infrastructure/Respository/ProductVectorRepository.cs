using MassTransit.Internals;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.Connectors.Milvus;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Memory;
using Milvus.Client;
using nucleotidz.recommendation.infrastructure.Interfaces;

namespace nucleotidz.recommendation.infrastructure.Respository
{
    public class ProductVectorRepository : IProductVectorRepository
    {
        private readonly string ProductCollection = "productcollection";
        private readonly ISemanticTextMemory memory;
        public ProductVectorRepository(IConfiguration configuration)
        {
            //configuration["AzureOpenAI:Endpoint"]
            //     configuration["AzureOpenAI:AuthKey"]
#pragma warning disable SKEXP0020 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            memory = new MemoryBuilder()
                    .WithAzureOpenAITextEmbeddingGeneration("vectoriser", "https://nucleo-tidz.openai.azure.com/", configuration["AzureOpenAI:AuthKey"])
                     .WithMemoryStore(new MilvusMemoryStore("standalone", metricType: SimilarityMetricType.Ip)).Build();
#pragma warning restore SKEXP0020 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        }
        public async Task SaveProductVector(string description, string productcode, string productName)
        {
            _ = await memory.SaveInformationAsync(ProductCollection, id: productcode, text: description, description: $"{productName}::${description}");
        }
        public async Task<IList<MemoryQueryResult>> Search(string description)
        {
            return await memory.SearchAsync(ProductCollection, description, limit: 10).ToListAsync();
        }
    }
}
