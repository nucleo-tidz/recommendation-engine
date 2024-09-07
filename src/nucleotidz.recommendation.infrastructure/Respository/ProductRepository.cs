using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.model;
using System.Data;
using System.Text.Json;

namespace nucleotidz.recommendation.infrastructure.Respository
{
    internal class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("nucleotidz");
        }
        public async Task<int> Save(IEnumerable<ProductEntity> productEntities)
        {
            DataTable productTable = new();
            _ = productTable.Columns.Add("Code", typeof(string));
            _ = productTable.Columns.Add("Name", typeof(string));
            _ = productTable.Columns.Add("Description", typeof(string));
            productEntities.ToList().ForEach(_ =>
            {
               productTable.Rows.Add(_.Code, _.Name, _.Description);
            });
            string sql = @"MERGE dbo.Product AS t
		                USING @product AS s
		                on t.Code = s.Code
		                WHEN MATCHED  THEN
			                UPDATE SET
		                     t.Name = s.Name, 
			                 t.Description = s.Description			
		                WHEN NOT MATCHED THEN
		                    INSERT (Code, Name, Description)
			                VALUES (s.Code, s.Name, s.Description);";
            await using SqlConnection connection = new(_connectionString);
            return await connection.ExecuteAsync(sql, new { product = productTable.AsTableValuedParameter("[dbo].[Product_DataType]") });
        }
        public async Task<IEnumerable<ProductEntity>> Get()
        {
            string sql = @"select Code,Name,Description from dbo.Product";
            await using SqlConnection connection = new(_connectionString);
            return await connection.QueryAsync<ProductEntity>(sql);
        }

        public async Task<string> Get(string Email)
        {
            string sql = @"select p.Description from dbo.Product p
                       join  dbo.[Order] o on  o.ProductCode=p.Code
                       where o.CustomerEmail=@email";
            await using SqlConnection connection = new(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<string>(sql, new { email = Email });
        }
    }
}
