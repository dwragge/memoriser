using System.Threading.Tasks;

namespace Memoriser.App.Commands
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync(ICommand command);
    }
}
