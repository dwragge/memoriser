using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Memoriser.App.Query.Handlers;
using Memoriser.App.Query.Queries;
using Memoriser.ApplicationCore.Models;
using Memoriser.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Memoriser.UnitTests.API.Queries
{
    public class GetWordByNameQueryHandlerTests
    {
        [Fact]
        public async Task Can_GetWordByName()
        {
            var options = new DbContextOptionsBuilder<LearningItemContext>()
                .UseInMemoryDatabase("Get_One")
                .Options;
            var items = new List<LearningItem>
            {
                new LearningItem("en colère", "angry"),
                new LearningItem("piste", "runway")
            };
            using (var context = new LearningItemContext(options))
            {
                context.AddRange(items);
                context.SaveChanges();
            }

            using (var context = new LearningItemContext(options))
            {
                var handler = new GetWordByNameQueryHandler(context);
                var query = new GetWordByNameQuery("en colère");

                var result = await handler.HandleAsync(query);

                result.ToBeGuessed.Should().Be("en colère");
            }
        }

        [Fact]
        public async Task Should_ReturnNullForNotFound()
        {
            var options = new DbContextOptionsBuilder<LearningItemContext>()
                .UseInMemoryDatabase("Get_One")
                .Options;
            var items = new List<LearningItem>
            {
                new LearningItem("en colère", "angry"),
                new LearningItem("piste", "runway")
            };
            using (var context = new LearningItemContext(options))
            {
                context.AddRange(items);
                context.SaveChanges();
            }

            using (var context = new LearningItemContext(options))
            {
                var query = new GetWordByNameQuery("bonjour");
                var handler = new GetWordByNameQueryHandler(context);

                var result = await handler.HandleAsync(query);

                result.Should().BeNull();
            }
        }
    }
}
