using FluentAssertions;
using Memoriser.App.Commands.Commands;
using Xunit;

namespace Memoriser.UnitTests.API.Commands
{
    public class AddWordCommandTests
    {
        [Fact]
        public void Should_TrimInput()
        {
            var answers = new[] {" spAces ", "       Extra    spaces    "};
            var word = " WoRd     ";
            var command = new AddWordCommand(word, answers);
            command.Word.Should().Be("word");
            command.AcceptedAnswers.Should().BeEquivalentTo(new string[] {"spaces", "extra spaces"});
        }
    }
}
