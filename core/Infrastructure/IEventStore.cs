using core.events;

namespace core.Infrastructure
{
    public interface IEventStore
    {
        Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion);

        Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId);
    }
}
