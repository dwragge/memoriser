using FluentAssertions;
using Memoriser.App.Commands;
using Memoriser.App.Commands.Commands;
using Memoriser.App.Commands.Handlers;
using Memoriser.App.Controllers;
using Memoriser.App.Query;
using Memoriser.App.Query.Handlers;
using Memoriser.App.Query.Queries;
using Memoriser.ApplicationCore.Models;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Memoriser.UnitTests.API.Controllers
{
    public class WordsControllerTests
    {
        [Fact]
        public async Task Should_ReturnAll()
        {
            var words = new[]
            {
                new LearningItem("salut", new [] {"hello, goodbye" }),
                new LearningItem("maison", "house")
            };

            var mockHandler = new Mock<IAsyncQueryHandler<GetWordsQuery, LearningItem[]>>();
            mockHandler.Setup(x => x.HandleAsync(It.IsAny<GetWordsQuery>())).Returns(Task.FromResult(words));
            var controller = new WordsController(mockHandler.Object, null);

            var result = await controller.Words();

            result.ShouldBeEquivalentTo(words);
        }

        [Fact]
        public async Task Should_AddWord()
        {
            var mockHandler = new Mock<IAsyncCommandHandler<AddWordCommand>>();
            var controller = new WordsController(null, mockHandler.Object);
            await controller.AddWord();
        }
    }
}
