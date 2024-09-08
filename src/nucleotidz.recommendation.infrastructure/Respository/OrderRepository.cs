using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.model;
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
            string sql = @"Insert into dbo.[order] values (@productCode,@customerEmail,GETUTCDATE())";
            await using SqlConnection connection = new(_connectionString);
            return await connection.ExecuteAsync(sql, new { productCode, customerEmail });
        }
        public async Task<IEnumerable<OrderEntity>> Get(string customerEmail)
        {
            string sql = @"SELECT [ProductCode] as code  ,p.[Name],p.Description  ,[OrderDate]  FROM [nucleotidz].[dbo].[Order] o join dbo.Product p on o.ProductCode=p.Code where customerEmail =@customerEmail";
            await using SqlConnection connection = new(_connectionString);
            return await connection.QueryAsync<OrderEntity>(sql, new { customerEmail });
        }

        public async Task<int> Delete(string productCode, string customerEmail)
        {
            string sql = @"Delete from dbo.[order] where ProductCode =@productCode and CustomerEmail =@customerEmail";
            await using SqlConnection connection = new(_connectionString);
            return await connection.ExecuteAsync(sql, new { productCode, customerEmail });
        }
    }
}
