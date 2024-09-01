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
        public async Task<int> Save(string productCode, float[] vectors)
        {
            string vector = JsonSerializer.Serialize(vectors);
            string sql = @"MERGE dbo.ProductVector AS target
                         USING (SELECT @code AS code, @vector AS vector) AS source
                             ON target.productcode = source.code
                         WHEN MATCHED THEN
                             UPDATE SET target.vector = source.vector
                         WHEN NOT MATCHED THEN
                             INSERT (productcode, vector) VALUES (source.code, source.vector);";
            await using SqlConnection connection = new(_connectionString);
            return await connection.ExecuteAsync(sql, new { code = productCode, vector });
        }
        public async Task<IEnumerable<ProductEntity>> Get()
        {
            string sql = @"select Code,Name,Description from dbo.Product";
            await using SqlConnection connection = new(_connectionString);
            return await connection.QueryAsync<ProductEntity>(sql);
        }

        public async Task<string> Get(string Email)
        {
            string sql = @"select pv.Vector from dbo.Product p
                       join dbo.ProductVector pv on p.Code=pv.ProductCode
                       join  dbo.[Order] o on  o.ProductCode=p.Code
                       where o.CustomerEmail=@email";
            await using SqlConnection connection = new(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<string>(sql, new { email = Email });
        }
    }
}
