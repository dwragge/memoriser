using Memoriser.App.Query.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Memoriser.App.Commands;
using Memoriser.App.Commands.Commands;
using Memoriser.App.Query;
using Memoriser.ApplicationCore.Models;

namespace Memoriser.App.Controllers
{
    [Route("api/[controller]")]
    public class WordsController
    {
        private readonly IAsyncQueryHandler<GetWordsQuery, LearningItem[]> _getItemsHandler;
        private readonly IAsyncCommandHandler<AddWordCommand> _AddItemHandler;

        public WordsController(IAsyncQueryHandler<GetWordsQuery, LearningItem[]> handler, IAsyncCommandHandler<AddWordCommand> addHandler1)
        {
            _getItemsHandler = handler;
            _AddItemHandler = addHandler1;
        }

        [HttpGet]
        public async Task<LearningItem[]> Words()
        {
            var query = new GetWordsQuery();
            return await _getItemsHandler.HandleAsync(query);
        }

        [HttpPost]
        public async Task AddWord()
        {
            var command = new AddWordCommand("salut", new []{"hey", "yo"});
            await _AddItemHandler.HandleAsync(command);
        }
    }
}
