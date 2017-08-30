using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memoriser.App.Commands.Commands
{
    public interface ICommandHandlerResolver
    {
        IAsyncCommandHandler<TCommand> Resolve<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
