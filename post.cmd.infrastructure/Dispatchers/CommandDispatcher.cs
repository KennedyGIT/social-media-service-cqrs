
using core.commands;
using core.Infrastructure;

namespace post.cmd.infrastructure.Dispatchers
{
    /// <summary>
    /// The CommandDispatcher class allows you to register and dispatch handlers for different types of commands. 
    /// It's a centralized way to manage and execute commands without directly coupling commands and their handlers. 
    /// By using the Dictionary<Type, Func<BaseCommand, Task>> structure, 
    /// it's able to associate command types with corresponding handler functions, 
    /// providing a clean and organized way to manage command execution in various scenarios.
    /// </summary>
    public class CommandDispatcher : ICommandDispatcher
    {
        /// <summary>
        ///  This private field _handlers is a dictionary that associates command types
        ///  (Type) with their corresponding handlers (Func<BaseCommand, Task>). This allows the 
        ///  class to store and look up handlers for different types of commands
        /// </summary>
        private readonly Dictionary<Type, Func<BaseCommand, Task>> _handlers = new();

        /// <summary>
        /// This method allows you to register a handler for a specific type of command. It takes a generic parameter T that must be 
        /// derived from BaseCommand. The provided handler function takes an instance of type T and returns a Task. The method checks 
        /// if a handler for the same type has already been registered and throws an exception if so. It then associates the command 
        /// type with the handler in the _handlers dictionary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand
        {
            if(_handlers.ContainsKey(typeof(T))) 
            {
                throw new IndexOutOfRangeException("You cannot register the same command handler twice!");
            }

            _handlers.Add(typeof(T), x => handler((T)x));
        }

        /// <summary>
        /// This method is used to send a command for processing. It takes a BaseCommand instance as a parameter. 
        /// The method checks the dictionary to see if there's a registered handler for the type of the given command. 
        /// If a handler is found, it invokes the handler with the command instance and awaits its execution. 
        /// If no handler is registered for the command type, an exception is thrown.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task SendAsync(BaseCommand command)
        {
            if (_handlers.TryGetValue(command.GetType(), out Func<BaseCommand, Task> handler))
            {
                await handler(command);
            }
            else 
            {
                throw new ArgumentException(nameof(handler), "No command handler was registered");
            }
        }
    }
}
