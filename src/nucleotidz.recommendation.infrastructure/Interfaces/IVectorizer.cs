namespace nucleotidz.recommendation.infrastructure.Interfaces
{
    public interface IVectorizer
    {
        Task<IList<ReadOnlyMemory<float>>> GenerateEmbeddingsAsync(string[] input);
    }
}
