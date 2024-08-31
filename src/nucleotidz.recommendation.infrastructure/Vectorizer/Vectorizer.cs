using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using nucleotidz.recommendation.infrastructure.Interfaces;

namespace nucleotidz.recommendation.infrastructure
{
    public class Vectorizer(Kernel kernel) : IVectorizer
    {
        public async Task GenerateEmbeddingsAsync(string[] input)
        {
            var vector = await (kernel.GetRequiredService<AzureOpenAITextEmbeddingGenerationService>()).GenerateEmbeddingsAsync(input);
        }
    }
}
