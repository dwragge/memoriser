using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Memoriser.App.Controllers.PostModels;
using Xunit;

namespace Memoriser.UnitTests.API.Controllers
{
    public class AddWordPostModelTests
    {
        public static IEnumerable<object[]> InvalidModels
        {
            get
            {
                yield return new object[] {new AddWordPostModel()};
                yield return new object[] {new AddWordPostModel {Answers = null, Word = "Hello"}};
                yield return new object[] {new AddWordPostModel {Answers = new[] {"Hello", "Goodbye"}, Word = null}};
                yield return new object[] {new AddWordPostModel {Answers = new string[] { }, Word = "Hello"}};
                yield return new object[] {new AddWordPostModel {Answers = new[] {"Hello"}, Word = "123"}};
                yield return new object[] {new AddWordPostModel {Answers = new[] {"123"}, Word = "Hello"}};
                yield return new object[] {new AddWordPostModel {Answers = new[] {"abc", "123 &&^"}, Word = "Hello"}};
                yield return new object[] {new AddWordPostModel {Answers = new[] {"Hello"}, Word = " "}};
            }
        }

        [Theory]
        [InlineData(new [] {" "}, "Hello")]
        [InlineData(new[] { "Hello", "Goodbye" }, null)]
        [InlineData(new string[] { }, "Hello")]
        [InlineData(new[] { "Hello" }, "123")]
        [InlineData(new[] { "123" }, "Hello")]
        [InlineData(new[] { "abc", "123 &&^" }, "Hello")]
        [InlineData(new[] { "Hello" }, " ")]
        public void Validation_Should_FailForInvalid(string[] answers, string word)
        {
            var model = new AddWordPostModel()
            {
                Answers = answers,
                Word = word
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, results, true);
            results.Count.Should().BeGreaterThan(0, $"Answers: \"{string.Join(" ,", answers)}\" and Word: \"{word}\" should fail the validation.");
        }
    }
}
