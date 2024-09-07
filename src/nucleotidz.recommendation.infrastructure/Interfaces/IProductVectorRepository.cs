using Microsoft.SemanticKernel.Memory;

namespace nucleotidz.recommendation.infrastructure.Interfaces
{
    public interface IProductVectorRepository
    {
        Task SaveProductVector(string description, string productcode, string productName);
        Task<IList<MemoryQueryResult>> Search(string description);
    }
}
