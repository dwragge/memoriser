using Memoriser.App.Query.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Memoriser.App.Commands;
using Memoriser.App.Commands.Commands;
using Memoriser.App.Query;
using Memoriser.ApplicationCore.Models;
using Memoriser.App.Controllers.PostModels;
using System.Linq;
using System;

namespace Memoriser.App.Controllers
{
    [Route("api/[controller]")]
    public class WordsController : Controller
    {
        private readonly IAsyncCommandHandler<AddWordCommand> _addWordCommandHandler;
        private readonly IAsyncQueryHandler<FindItemsQuery, LearningItem[]> _findItemsQueryHandler;

        public WordsController(IAsyncCommandHandler<AddWordCommand> addWordHandler,
                                IAsyncQueryHandler<FindItemsQuery, LearningItem[]> findQueryHandler)
        {
            _addWordCommandHandler = addWordHandler;
            _findItemsQueryHandler = findQueryHandler;
        }

        [HttpGet]
        public async Task<LearningItem[]> Words()
        { 
            var result = await _findItemsQueryHandler.QueryAsync(FindItemsQuery.All);
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> AddWord([FromBody]AddWordPostModel postData)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var command = new AddWordCommand(postData.Word, postData.Answers);
            await _addWordCommandHandler.HandleAsync(command);

            var query = FindItemsQuery.ByWord(postData.Word);
            var queryResult = await _findItemsQueryHandler.QueryAsync(query);
            var createdItem = queryResult.Single();

            string currentUri = Request?.Path ?? "/Words";
            return new CreatedResult(currentUri.JoinPaths(createdItem.Id.ToString()), createdItem);
        }
    }
}
