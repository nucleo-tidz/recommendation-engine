using Milvus.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nucleotidz.recommendation.engine.schema.manager
{
    public static class VectorSchema
    {
        public static Dictionary<string, CollectionSchema> Schemas = new Dictionary<string, CollectionSchema>
        {
            { "products",ProductSchema.schema}
        };
    }
}
