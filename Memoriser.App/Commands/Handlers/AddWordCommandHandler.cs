using System.Threading.Tasks;
using Memoriser.App.Commands.Commands;
using Memoriser.ApplicationCore.LearningItems;
using Memoriser.Infrastructure;

namespace Memoriser.App.Commands.Handlers
{
    public class AddWordCommandHandler : IAsyncCommandHandler<AddWordCommand>
    {
        private readonly LearningItemContext _context;
        public AddWordCommandHandler(LearningItemContext context)
        {
            _context = context;
        }

        public async Task HandleAsync(AddWordCommand command)
        {
            var item = new LearningItem(command.Word, command.AcceptedAnswers);
            _context.LearningItems.Add(item);
            await _context.SaveChangesAsync();
        }
    }
}
