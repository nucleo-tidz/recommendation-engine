using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.model;
using System.Data;

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
            DataTable productTable = new DataTable();
            _ = productTable.Columns.Add("Code", typeof(string));
            _ = productTable.Columns.Add("Name", typeof(string));
            _ = productTable.Columns.Add("Description", typeof(string));
            productEntities.ToList().ForEach(_ =>
            {
                productTable.Rows.Add(_.Code, _.Name, _.Description);
            });
            var sql = @"MERGE dbo.Product AS t
		                USING @product AS s
		                on t.Code = s.Code
		                WHEN MATCHED  THEN
			                UPDATE SET
		                     t.Name = s.Name, 
			                 t.Description = s.Description			
		                WHEN NOT MATCHED THEN
		                    INSERT (Code, Name, Description)
			                VALUES (s.Code, s.Name, s.Description);";
            await using var connection = new SqlConnection(_connectionString);
            return await connection.ExecuteAsync(sql, new { product = productTable.AsTableValuedParameter("[dbo].[Product_DataType]") });
        }
    }
}
