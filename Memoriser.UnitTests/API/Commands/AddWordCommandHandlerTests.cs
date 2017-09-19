using FluentAssertions;
using Memoriser.App.Commands.Commands;
using Memoriser.App.Commands.Handlers;
using Memoriser.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Memoriser.UnitTests.API.Commands
{
    public class AddWordCommandHandlerTests
    {
        [Theory]
        [InlineData("salut", new[] { "hello" })]
        [InlineData("parvenir", new[] { "reach", "succeed" })]
        [InlineData("éloigner", new[] { "put off", "postpone" })]
        public async Task Should_AddNewWord(string word, string[] answers)
        {
            var options = new DbContextOptionsBuilder<LearningItemContext>()
                .UseInMemoryDatabase("Add_Item")
                .Options;

            using (var context = new LearningItemContext(options))
            {
                var command = new AddWordCommand(word, answers);
                var handler = new AddWordCommandHandler(context);
                await handler.HandleAsync(command);
            }

            using (var context = new LearningItemContext(options))
            {
                var items = await context.LearningItems.ToListAsync();

                var item = items.Single(x => x.ToBeGuessed == word && x.AcceptedAnswers.SequenceEqual(answers));
                item.Should().NotBeNull();
                item.ToBeGuessed.ShouldBeEquivalentTo(word);
                item.AcceptedAnswers.ShouldAllBeEquivalentTo(answers);
            }
        }
    }
}
