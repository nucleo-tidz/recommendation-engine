namespace nucleotidz.recommendation.infrastructure.Interfaces
{
    public interface IProductVectorRepository
    {
        Task SaveProductVector(ReadOnlyMemory<float>[] vectors, string productcode, string productName);
        Task Search(ReadOnlyMemory<float>[] vectors);
    }
}
