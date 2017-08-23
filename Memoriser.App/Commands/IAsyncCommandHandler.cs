using System.Threading.Tasks;

namespace Memoriser.App.Commands
{
    public interface IAsyncCommandHandler<in TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}
