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
        private readonly IAsyncQueryHandler<GetWordsQuery, LearningItem[]> _getItemsHandler;
        private readonly IAsyncCommandHandler<AddWordCommand> _AddItemHandler;
        private readonly IQueryHandlerResolver _queryResolver;

        public WordsController(IAsyncQueryHandler<GetWordsQuery, LearningItem[]> handler, IAsyncCommandHandler<AddWordCommand> addHandler1)
        {
            _getItemsHandler = handler;
            _AddItemHandler = addHandler1;
        }

        [HttpGet]
        public async Task<LearningItem[]> Words()
        {
            var query = new GetWordsQuery();
            var handler = _queryResolver.Resolve(query);
            return await handler.HandleAsync(query);
        }

        [HttpPost]
        public async Task<IActionResult> AddWord([FromBody]AddWordPostModel postData)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var validWords = postData.Answers.ToList().TrueForAll(word => word.IsOnlyLetterCharacters());
            if (!validWords)
            {
                return new BadRequestObjectResult(postData.Answers);
            }

            var command = new AddWordCommand(postData.Word, postData.Answers);
            await _AddItemHandler.HandleAsync(command);
            var currentUri = Url.RouteUrl(RouteData.Values);
            return new CreatedResult("", "");
        }
    }
}
