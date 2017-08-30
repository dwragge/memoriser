using FluentAssertions;
using Memoriser.App.Commands;
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
            
            var mockHandler = new Mock<IAsyncCommandHandler<AddWordCommand>>();
            var controller = new WordsController(null, mockHandler.Object);

            var result = await controller.AddWord(data);

            result.Should().BeOfType<CreatedResult>();
            var createdResult = result as CreatedResult;
            createdResult.StatusCode.Should().Be(201);
            createdResult.Location.Should().MatchRegex(@"\/Words\/[{(]?[0-9A-F]{8}[-]?([0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?");
        }

        [Theory]
        [InlineData("éteindre", new[] { "abc", "123 a$c" })]
        [InlineData("maître", new[] { "&&^", "@$@!" })]
        public async Task Should_ErrorForInvalidWordData(string word, string[] answers)
        {
            var data = new AddWordPostModel
            {
                Word = word,
                Answers = answers
            };
            var mockHandler = new Mock<IAsyncCommandHandler<AddWordCommand>>();
            var controller = new WordsController(null, mockHandler.Object);

            var result = await controller.AddWord(data);

            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
