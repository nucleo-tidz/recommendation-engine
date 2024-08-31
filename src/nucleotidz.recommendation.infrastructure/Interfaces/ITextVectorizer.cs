namespace nucleotidz.recommendation.infrastructure.Interfaces
{
    public interface ITextVectorizer
    {
        Task<IList<ReadOnlyMemory<float>>> GenerateEmbeddingsAsync(string[] input);
    }
}
