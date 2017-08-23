using Memoriser.App.Query.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memoriser.App.Controllers
{
    [Route("api/[controller]")]
    public class WordsController
    {
        [HttpGet]
        public IEnumerable<string> Words()
        {
            var query = new GetRequiredLearningItemsQuery();
            return new List<string>
            {
                "hello", "bonjour", "le monde"
            };
        }
    }
}
