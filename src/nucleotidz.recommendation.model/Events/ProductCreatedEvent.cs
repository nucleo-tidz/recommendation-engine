namespace nucleotidz.recommendation.model.Events
{
    public class ProductCreatedEvent : IEvent
    {
        public required string @event { get; set; }
        public DateTime EventDate { get; set; } = DateTime.UtcNow;

        public required string Code { get; set; }
        public required string Description { get; set; }
        public required string Name { get; set; }
    }
}
