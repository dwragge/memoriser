using System.Threading.Tasks;
using Autofac;

namespace Memoriser.App.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ILifetimeScope _container;
        public CommandDispatcher(ILifetimeScope container)
        {
            _container = container;
        }
        public Task DispatchAsync(ICommand command)
        {
            var handlerType = typeof(IAsyncCommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = _container.Resolve(handlerType);
            return handler.HandleAsync((dynamic) command);
        }
    }
}
