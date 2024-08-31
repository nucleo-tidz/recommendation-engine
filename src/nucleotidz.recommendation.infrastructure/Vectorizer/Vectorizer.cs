using nucleotidz.recommendation.infrastructure.Interfaces;

namespace nucleotidz.recommendation.infrastructure
{
    public class Vectorizer(IVectorizerFactory vectorizerFactory) : IVectorizer
    {
        public async Task<IList<ReadOnlyMemory<float>>> GenerateEmbeddingsAsync(string[] input)
        {
            return await vectorizerFactory.Create().GenerateEmbeddingsAsync(input);
        }
    }
}
