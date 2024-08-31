﻿using Milvus.Client;

namespace nucleotidz.recommendation.infrastructure.Vectorizer.Schema
{
    public static class ProductSchema
    {
        public static CollectionSchema schema = new CollectionSchema
        {
            Fields =
            {
              FieldSchema.CreateVarchar("product_code", maxLength: 200, isPrimaryKey: true),
              FieldSchema.CreateVarchar("product_name", maxLength: 1000),
               FieldSchema.CreateFloatVector("product_description", dimension: 2)
            }
        };
    }
}
