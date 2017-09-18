using FluentAssertions;
using Memoriser.App.Commands.Commands;
using Memoriser.App.Controllers;
using Memoriser.App.Controllers.PostModels;
using Memoriser.App.Query;
using Memoriser.App.Query.Queries;
using Memoriser.ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Memoriser.UnitTests.API.Controllers
{
    public class WordsControllerTests
    {
        public Mock<IAsyncQueryHandler<FindItemsQuery, LearningItem[]>> MockFindItemsQueryHandler(LearningItem[] words)
        {
            var mock = new Mock<IAsyncQueryHandler<FindItemsQuery, LearningItem[]>>();
            mock.Setup(x => x.QueryAsync(It.IsAny<FindItemsQuery>())).Returns(Task.FromResult(words));
            return mock;
        }

        [Fact]
        public async Task Should_ReturnAll()
        {
            var words = new[]
            {
                new LearningItem("salut", new [] {"hello, goodbye" }),
                new LearningItem("maison", "house")
            };
            var mockHandler = new MockQueryHandler<FindItemsQuery, LearningItem[]>().ReturnsForAll(words).Handler;
            var controller = new WordsController(null, mockHandler);

            var result = await controller.Words();

            result.ShouldBeEquivalentTo(words);
        }

        [Theory]
        [InlineData("éteindre", new[] { "turn off", "extinguish"})]
        [InlineData("maître", new[] { "master" })]
        public async Task Should_AddWord(string word, string[] answers)
        {
            var data = new AddWordPostModel
            {
                Word = word,
                Answers = answers
            };
            var mockCommandHandler = new MockCommandHandler<AddWordCommand>().ReturnsForAll().Handler;
            var mockQueryHandler = new MockQueryHandler<FindItemsQuery, LearningItem[]>()
                .ReturnsForAll(new [] {new LearningItem(word, answers) })
                .Handler;
            var controller = new WordsController(mockCommandHandler, mockQueryHandler);

            var result = await controller.AddWord(data);

            result.Should().BeOfType<CreatedResult>();
            var createdResult = (CreatedResult)result;
            createdResult.StatusCode.Should().Be(201);
            createdResult.Location.Should().MatchRegex(@"\/Words\/[{(]?[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}[)}]?");
        }

        [Fact]
        public async Task Should_Error_ForInvalidModelState()
        {
            var data = new AddWordPostModel
            {
                Word = "Bonjour",
                Answers = new []{"Hello", "Good Morning"}
            };

            var mockHandler = new MockCommandHandler<AddWordCommand>().ReturnsForAll().Handler;
            var controller = new WordsController(mockHandler, null);
            controller.ModelState.AddModelError(string.Empty, "Something is wrong!");

            var result = await controller.AddWord(data);

            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
