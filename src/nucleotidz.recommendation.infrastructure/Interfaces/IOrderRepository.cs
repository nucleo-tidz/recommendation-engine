namespace nucleotidz.recommendation.infrastructure.Interfaces
{
    public interface IOrderRepository
    {
        public Task<int> Create(string productCode, string customerEmail);
    }
}
