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
    public class GetWordsQueryHandlerTests
    {
        private readonly DbContextOptions<LearningItemContext> _options;
        private readonly List<LearningItem> _items = new List<LearningItem>
        {
            new LearningItem("salut", "hello"),
            new LearningItem("générer", "generate"),
            new LearningItem("atteindre", new[] { "reach", "achieve" })
            {
                Interval = RepetitionInterval.FromValues(3, 3.0f)
            }
        };

        public GetWordsQueryHandlerTests()
        {
            _options = new DbContextOptionsBuilder<LearningItemContext>()
                .UseInMemoryDatabase("Get_All")
                .Options;

            using (var context = new LearningItemContext(_options))
            {
                context.AddRange(_items);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task Should_Return_AllItems()
        {
            using (var context = new LearningItemContext(_options))
            {
                var handler = new FindItemsQueryHandler(context);
                var result = await handler.QueryAsync(FindItemsQuery.All);
                result.ToList().ShouldAllBeEquivalentTo(_items);
            }
        }

        [Fact]
        public async Task Should_Return_BasedOnQuery()
        {
            using (var context = new LearningItemContext(_options))
            {
                var handler = new FindItemsQueryHandler(context);
                var result = await handler.QueryAsync(FindItemsQuery.ByWord("salut"));
                var item = result.Single();
                item.ShouldBeEquivalentTo(_items[0]);
            }
        }
    }
}
