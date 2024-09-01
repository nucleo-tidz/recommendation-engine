using nucleotidz.recommendation.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nucleotidz.recommendation.infrastructure.Interfaces
{
    public interface IProductRepository
    {
        Task<int> Save(IEnumerable<ProductEntity> productEntities);
        Task<int> Save(string productCode, float[] vectors);
    }
}
