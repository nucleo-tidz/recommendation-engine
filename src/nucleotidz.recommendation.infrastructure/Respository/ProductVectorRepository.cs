using Milvus.Client;
using nucleotidz.recommendation.infrastructure.Interfaces;
using nucleotidz.recommendation.infrastructure.Vectorizer;

namespace nucleotidz.recommendation.infrastructure.Respository
{
    public class ProductVectorRepository : VectorHelper, IProductVectorRepository
    {

        public async Task SaveProductVector(ReadOnlyMemory<float>[] vectors, string productcode, string productName)
        {

            MilvusCollection milvusCollection;
            if (await base.HasCollection("products"))
            {
                milvusCollection = GetCollection("products");
             
            }
            milvusCollection = await CreateCollection("products", infrastructure.Vectorizer.Schema.ProductSchema.schema);

            var result = await milvusCollection.UpsertAsync(new FieldData[]
                                         {

                                                  FieldData.Create("product_code",new string[]{ productcode}),
                                                  FieldData.Create("product_name", new string[]{ productName}),
                                                  FieldData.CreateFloatVector("product_description", vectors)
                                         });

        }
    }

}
