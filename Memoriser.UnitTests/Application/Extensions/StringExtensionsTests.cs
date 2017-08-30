using FluentAssertions;
using System;
using Xunit;

namespace Memoriser.UnitTests.Application.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("http://www.example.com", "part1", "http://www.example.com/part1")]
        [InlineData("http://www.example.com/", "part1", "http://www.example.com/part1")]
        [InlineData("http://www.example.com", "/part1", "http://www.example.com/part1")]
        [InlineData("http://www.example.com", "part1/part2", "http://www.example.com/part1/part2")]
        [InlineData("http://www.example.com/part1", "part2/part3", "http://www.example.com/part1/part2/part3")]
        [InlineData("http://www.example.com", "", "http://www.example.com")]
        [InlineData("", "part1/part2", "/part1/part2")]
        public void Should_JoinPaths(string basePart, string relativePart, string expected)
        {
            var joined = basePart.JoinPaths(relativePart);
            joined.Should().Be(expected);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("abc abc")]
        [InlineData("áêèçabc abc")]
        public void Should_ReturnTrueForLetterStrings(string input)
        {
            var result = input.IsOnlyLetterCharacters();
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("111")]
        [InlineData("abc 111")]
        [InlineData("abc1")]
        [InlineData("ab2b")]
        [InlineData("111 111")]
        public void Should_ReturnFalseForNonLetterString(string input)
        {
            var result = input.IsOnlyLetterCharacters();
            result.Should().BeFalse();
        }
    }
}
