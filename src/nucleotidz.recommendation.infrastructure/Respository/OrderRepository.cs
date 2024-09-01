using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using nucleotidz.recommendation.infrastructure.Interfaces;

namespace nucleotidz.recommendation.infrastructure.Respository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;
        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("nucleotidz");
        }
        public async Task<int> Create(string productCode, string customerEmail)
        {
            string sql = @"Insert into dbo.[order] values (@productCode,@customerEmail)";
            await using SqlConnection connection = new(_connectionString);
            return await connection.ExecuteAsync(sql, new { productCode, customerEmail });
        }
    }
}
