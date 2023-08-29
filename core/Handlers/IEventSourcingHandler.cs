using core.Domain;

namespace core.Handlers
{
    public interface IEventSourcingHandler<T>
    {
        Task SaveAsync(AggregateRoot aggregate);

        Task<T> GetByIdAsync(Guid aggregateId);
    }
}
