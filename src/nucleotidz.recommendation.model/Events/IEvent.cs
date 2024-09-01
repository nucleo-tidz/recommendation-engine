namespace nucleotidz.recommendation.model.Events
{
    public interface IEvent
    {
        string @event { get; set; }
        DateTime EventDate { get; set; }
    }
}
