namespace nucleotidz.recommendation.infrastructure.Interfaces
{
    public interface IEventPublisher

    {
        Task Publish<T>(T @event)
            where T : class;
    }
}
