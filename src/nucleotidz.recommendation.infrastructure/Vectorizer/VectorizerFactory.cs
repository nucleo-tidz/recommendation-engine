using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Embeddings;
using nucleotidz.recommendation.infrastructure.Interfaces;

namespace nucleotidz.recommendation.infrastructure.Vectorizer
{
    public class VectorizerFactory(Kernel kernel) : IVectorizerFactory
    {
        public ITextEmbeddingGenerationService Create()
        {
            return kernel.GetRequiredService<ITextEmbeddingGenerationService>();
        }
    }
}
