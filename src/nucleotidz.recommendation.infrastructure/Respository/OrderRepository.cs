using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using nucleotidz.recommendation.infrastructure.Interfaces;
using System.Text.Json;

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
            var sql = @"Insert into dbo.[order] values (@productCode,@customerEmail)";
            await using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync(sql, new { productCode = productCode, customerEmail = customerEmail });
        }
    }
}
