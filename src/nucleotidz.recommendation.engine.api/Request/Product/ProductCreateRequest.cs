using nucleotidz.recommendation.model;
using System.ComponentModel.DataAnnotations;

namespace nucleotidz.recommendation.engine.api.Request.Product
{
    public class ProductCreateRequest
    {

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public static class ProductEntityConvertor
    {
        public static ProductEntity ToModel(this ProductCreateRequest request)
        {
            return new ProductEntity
            {
                Code = request.Code,
                Name = request.Name,
                Description = request.Description
            };
        }
    }
}
