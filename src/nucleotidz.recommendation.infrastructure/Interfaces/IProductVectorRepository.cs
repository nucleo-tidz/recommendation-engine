namespace nucleotidz.recommendation.infrastructure.Interfaces
{
    public interface IProductVectorRepository
    {
        Task SaveProductVector(ReadOnlyMemory<float>[] vectors, string productcode, string productName);
        Task<IEnumerable<string>> Search(ReadOnlyMemory<float>[] vectors);
    }
}
