namespace Memoriser.App.Commands.Commands
{
    public interface ICommandHandlerResolver
    {
        IAsyncCommandHandler<TCommand> Resolve<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
