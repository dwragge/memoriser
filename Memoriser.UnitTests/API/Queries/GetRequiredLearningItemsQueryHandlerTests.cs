using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Memoriser.ApplicationCore.Models;
using System.Linq;
using Memoriser.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Memoriser.App.Query.Handlers;
using Memoriser.App.Query.Queries;
using FluentAssertions;

namespace Memoriser.UnitTests.API.Queries
{
    public class GetRequiredLearningItemsQueryHandlerTests
    {
        [Fact]
        public async Task Should_ReturnAllItems()
        {
            var options = new DbContextOptionsBuilder<LearningItemContext>()
                .UseInMemoryDatabase("Get_All")
                .Options;

            var items = new List<LearningItem>
            {
                new LearningItem("salut", "hello"),
                new LearningItem("générer", "generate"),
                new LearningItem("atteindre", new[] { "reach", "achieve" })
                {
                    Interval = RepetitionInterval.FromValues(3, 3.0f)
                }
            };

            using (var context = new LearningItemContext(options))
            {
                context.AddRange(items);
                context.SaveChanges();
            }

            using (var context = new LearningItemContext(options))
            {
                var handler = new GetWordsQueryHandler(context);
                var result = await handler.HandleAsync(new GetWordsQuery());
                result.ToList().ShouldAllBeEquivalentTo(items);
            }
        }
    }
}
