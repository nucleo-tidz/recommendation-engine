﻿using Milvus.Client;
using nucleotidz.recommendation.infrastructure.Interfaces;

namespace nucleotidz.recommendation.infrastructure.Respository
{
    public class ProductVectorRepository(IVectorDatabaseHelper vectorDatabaseHelper) : IProductVectorRepository
    {
        public async Task SaveProductVector(ReadOnlyMemory<float>[] vectors, string productcode, string productName)
        {
            if (await vectorDatabaseHelper.HasCollection("products"))
            {
                MilvusCollection milvusCollection = vectorDatabaseHelper.GetCollection("products");
                _ = await milvusCollection.UpsertAsync(new FieldData[]
                                       {

                                                  FieldData.Create("product_code",new string[]{ productcode}),
                                                  FieldData.Create("product_name", new string[]{ productName}),
                                                  FieldData.CreateFloatVector("product_description", vectors)
                                       });
            }
        }
    }

}
