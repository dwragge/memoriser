using FluentAssertions;
using Memoriser.App.Controllers.PostModels;
using Xunit;

namespace Memoriser.UnitTests.API.Controllers
{
    public class AddWordPostModelTests : ValidatorTests
    {
        [Theory]
        [InlineData(new [] {" "}, " ")]
        [InlineData(new string[] { }, "123")]
        [InlineData(new[] { "123" }, "123 &&^")]
        public void Validation_Should_FailForInvalid(string[] answers, string word)
        {
            var model = new AddWordPostModel
            {
                Answers = answers,
                Word = word
            };

            ValidateObject(model).Count.Should().Be(2, 
                $"Answers: \"{string.Join(" ,", answers)}\" and Word: \"{word}\" should fail both validations.");
        }

        [Theory]
        [InlineData(new[] {"hello", "goodbye"}, "salut")]
        [InlineData(new[] {"éclarter", "rôder"}, "word")]
        public void Validation_Should_PassForValid(string[] answers, string word)
        {
            var model = new AddWordPostModel
            {
                Answers = answers,
                Word = word
            };

            ValidateObject(model).Count.Should().Be(0,
                $"Answers: \"{string.Join(" ,", answers)}\" and Word: \"{word}\" should pass the validation.");
        }
    }
}
