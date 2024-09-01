using Milvus.Client;

namespace nucleotidz.recommendation.engine.schema.manager.Schema
{
    public static class VectorSchema
    {
        public static Dictionary<string, CollectionSchema> Schemas = new()
        {
            { "products", ProductSchema.schema }
        };
    }
}
