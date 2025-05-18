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
