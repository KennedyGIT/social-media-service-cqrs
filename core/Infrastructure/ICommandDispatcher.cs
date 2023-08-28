using core.commands;

namespace core.Infrastructure
{
    public interface ICommandDispatcher
    {
        void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand;

        Task SendAsync(BaseCommand command);
    }
}
