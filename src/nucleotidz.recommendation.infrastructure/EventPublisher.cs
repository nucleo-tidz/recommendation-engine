using MassTransit;
using nucleotidz.recommendation.infrastructure.Interfaces;

namespace nucleotidz.recommendation.infrastructure
{
    internal class EventPublisher(IPublishEndpoint publishEndpoint) : IEventPublisher
    {
        public async Task Publish<T>(T @event) where T : class
        {
            await publishEndpoint.Publish(@event);
        }
    }
}
