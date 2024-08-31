using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nucleotidz.recommendation.infrastructure.Interfaces
{
    public interface IProductVectorRepository
    {
        Task SaveProductVector(ReadOnlyMemory<float>[] vectors, string productcode, string productName);
    }
}
