namespace nucleotidz.recommendation.infrastructure.Interfaces
{
    public interface IVectorizer
    {
        Task GenerateEmbeddingsAsync(string[] input);
    }
}
