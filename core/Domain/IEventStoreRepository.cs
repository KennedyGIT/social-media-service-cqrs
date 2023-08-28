using core.events;

namespace core.Domain
{
    public interface IEventStoreRepository
    {
        Task SaveAsync(EventModel @event);

        Task<List<EventModel>> FindByAggregate(Guid aggregateId);
    }
}
