namespace core.Consumers
{
    public interface IEventConsumer
    {
        void Consume(string topic);
    }
}
