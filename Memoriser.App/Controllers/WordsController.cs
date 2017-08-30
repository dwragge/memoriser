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
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryProcessor _queryProcessor;

        public WordsController(IQueryProcessor queryProcessor, ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryProcessor = queryProcessor;
        }

        [HttpGet]
        public async Task<LearningItem[]> Words()
        {
            var query = new GetWordsQuery();
            var result = await _queryProcessor.ProcessAsync(query);
            return result;
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
            await _commandDispatcher.DispatchAsync(command);
            var currentUri = Url.RouteUrl(RouteData.Values);
            return new CreatedResult("", "");
        }
    }
}
