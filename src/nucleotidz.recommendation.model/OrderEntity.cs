using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nucleotidz.recommendation.model
{
    public class OrderEntity
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
