using nucleotidz.recommendation.infrastructure.Interfaces;

namespace nucleotidz.recommendation.infrastructure.Vectorizer
{
    public class TextVectorizer(IVectorizerFactory vectorizerFactory) : ITextVectorizer
    {
        public async Task<IList<ReadOnlyMemory<float>>> GenerateEmbeddingsAsync(string[] input)
        {
            return await vectorizerFactory.Create().GenerateEmbeddingsAsync(input);
        }
    }
}
