namespace nucleotidz.recommendation.service.Interfaces
{
    public interface IOrderService
    {
        Task<int> Create(string productCode, string customerEmail);
    }
}
