using FluentAssertions;
using Memoriser.App.Controllers.PostModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Memoriser.UnitTests.API.Models
{
    public class AddWordPostModelTests
    {
        [Theory]
        [InlineData("éteindre", new[] { "turn off", "extinguish" })]
        [InlineData("être", new[] { "to be" })]
        [InlineData("maison", new[] { "house" })]
        public void Should_ValidateSuccessfully(string word, string[] answers)
        {
            var model = new AddWordPostModel
            {
                Answers = answers,
                Word = word
            };
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);

            valid.Should().BeTrue();
        }

        [Theory]
        [InlineData("maison", null)]
        [InlineData(null, new[] { "car", "bus" })]
        [InlineData("", new string[] { })]
        [InlineData("123 ab$c", new[] { "car" })]
        public void Should_FailValidation(string word, string[] answers)
        {
            var model = new AddWordPostModel
            {
                Answers = answers,
                Word = word
            };
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(model, context, result, true);

            valid.Should().BeFalse();
        }
    }
}
