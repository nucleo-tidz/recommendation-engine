using nucleotidz.recommendation.infrastructure.Interfaces;

namespace nucleotidz.recommendation.infrastructure
{
    public class Vectorizer(IVectorizerFactory vectorizerFactory) : IVectorizer
    {
        public async Task GenerateEmbeddingsAsync(string[] input)
        {
            IList<ReadOnlyMemory<float>> vector = await vectorizerFactory.Create().GenerateEmbeddingsAsync(input);
        }
    }
}
