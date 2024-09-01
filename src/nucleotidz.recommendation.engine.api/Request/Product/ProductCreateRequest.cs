using nucleotidz.recommendation.model;

namespace nucleotidz.recommendation.engine.api.Request.Product
{
    public class ProductCreateRequest
    {

        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
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
