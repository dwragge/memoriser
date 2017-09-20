using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Memoriser.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Memoriser.App.Query.Handlers;
using Memoriser.App.Query.Queries;
using FluentAssertions;
using Memoriser.ApplicationCore.LearningItems;

namespace Memoriser.UnitTests.API.Queries
{
    public class GetWordsQueryHandlerTestFixture
    {
        public readonly DbContextOptions<LearningItemContext> Options;
        public readonly List<LearningItem> Items = new List<LearningItem>
        {
            new LearningItem("salut", "hello"),
            new LearningItem("générer", "generate"),
            new LearningItem("atteindre", new[] { "reach", "achieve" })
            {
                Interval = RepetitionInterval.FromValues(3, 3.0f)
            }
        };
        public GetWordsQueryHandlerTestFixture()
        {
            Options = new DbContextOptionsBuilder<LearningItemContext>()
                .UseInMemoryDatabase("Get_All")
                .Options;

            using (var context = new LearningItemContext(Options))
            {
                context.AddRange(Items);
                context.SaveChanges();
            }
        }
    }

    public class GetWordsQueryHandlerTests : IClassFixture<GetWordsQueryHandlerTestFixture>
    {
        private readonly GetWordsQueryHandlerTestFixture _fixture;
        public GetWordsQueryHandlerTests(GetWordsQueryHandlerTestFixture textFixture)
        {
            _fixture = textFixture;
        }

        [Fact]
        public async Task Should_Return_AllItems()
        {
            using (var context = new LearningItemContext(_fixture.Options))
            {
                var handler = new FindItemsQueryHandler(context);
                var result = await handler.QueryAsync(FindItemsQuery.All);
                result.ToList().ShouldAllBeEquivalentTo(_fixture.Items);
            }
        }

        [Fact]
        public async Task Should_Return_BasedOnQuery()
        {
            using (var context = new LearningItemContext(_fixture.Options))
            {
                var handler = new FindItemsQueryHandler(context);
                var result = await handler.QueryAsync(FindItemsQuery.ByWord("salut"));
                var item = result.Single();
                item.ShouldBeEquivalentTo(_fixture.Items[0]);
            }
        }
    }
}
