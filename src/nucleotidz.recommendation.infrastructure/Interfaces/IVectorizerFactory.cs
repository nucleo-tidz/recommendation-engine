using Microsoft.SemanticKernel.Embeddings;

namespace nucleotidz.recommendation.infrastructure.Interfaces
{
    public interface IVectorizerFactory
    {
        ITextEmbeddingGenerationService Create();
    }
}
