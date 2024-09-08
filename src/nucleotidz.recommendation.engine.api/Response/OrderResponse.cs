using nucleotidz.recommendation.model;

namespace nucleotidz.recommendation.engine.api.Response
{
    public class OrderResponse
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string OrderDate { get; set; }
    }

    public static class OrderResponseConvertor
    {
        public static List<OrderResponse> ToOrderResponse(this IEnumerable<OrderEntity> entities)
        {
          return  entities.Select(e => new OrderResponse
            {
                Code = e.Code,
                Name = e.Name,
                Description = e.Description,
                OrderDate = e.OrderDate.ToString("yyyy-MM-dd")
            }).ToList();
        }
    }
}
