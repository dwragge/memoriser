using FluentAssertions;
using Memoriser.ApplicationCore.LearningItems;
using Xunit;

namespace Memoriser.UnitTests.Application.LearningItem
{
    public class RepetitionIntervalTests
    {
        [Fact]
        public void ProcessResponse_ShouldNot_UpdateEasinessFactorFirst3()
        {
            var interval = RepetitionInterval.NewDefault();
            interval.ProcessResponse(ResponseQuality.CorrectPerfect);
            interval.Interval.Should().Be(0);

            interval.ProcessResponse(ResponseQuality.CorrectPerfect);
            interval.Interval.Should().Be(0);

            interval.ProcessResponse(ResponseQuality.CorrectPerfect);
            interval.Interval.Should().Be(1);
        }

        [Theory]
        [InlineData(ResponseQuality.IncorrectBlackout)]
        [InlineData(ResponseQuality.IncorrectEasyRecalled)]
        [InlineData(ResponseQuality.IncorrectRemembered)]
        public void ProcessResponse_ShouldNot_UpdateStartBadAnswer(ResponseQuality badQuality)
        {
            var interval = RepetitionInterval.NewDefault();
            interval.ProcessResponse(badQuality);
            interval.ProcessResponse(badQuality);
            interval.ProcessResponse(badQuality);
            interval.Interval.Should().Be(0);

            interval.ProcessResponse(ResponseQuality.CorrectPerfect);
            interval.ProcessResponse(ResponseQuality.CorrectPerfect);
            interval.Interval.Should().Be(0);
        }

        [Theory]
        [InlineData(ResponseQuality.IncorrectBlackout)]
        [InlineData(ResponseQuality.IncorrectEasyRecalled)]
        [InlineData(ResponseQuality.IncorrectRemembered)]
        public void ProcessResponse_Should_ResetIntervalForBadAnswer(ResponseQuality qual)
        {
            var interval = RepetitionInterval.FromValues(4, 2.5f);
            interval.ProcessResponse(qual);
            interval.Interval.Should().Be(1);
        }

        [Fact]
        public void ProcessResponse_Should_UpdateIntervalFirstValues()
        {
            var interval = RepetitionInterval.FromValues(0, 2.5f);
            interval.ProcessResponse(ResponseQuality.CorrectPerfect);
            interval.Interval.Should().Be(1);
            interval.ProcessResponse(ResponseQuality.CorrectPerfect);
            interval.Interval.Should().Be(6);
        }

        [Fact]
        public void ProcessResponse_Should_UpdateInterval()
        {
            const int initialInterval = 3;
            var interval = RepetitionInterval.FromValues(initialInterval, 2.5f);
            interval.ProcessResponse(ResponseQuality.CorrectPerfect);
            interval.Interval.Should().BeGreaterThan(6);
            interval.EasinessFactor.Should().NotBe(2.5f);
        }

        [Fact]
        public void ProcessResponse_Should_Require3CorrectResponsesFirst()
        {
            var interval = RepetitionInterval.NewDefault();
            interval.ProcessResponse(ResponseQuality.CorrectPerfect);
            interval.ProcessResponse(ResponseQuality.IncorrectBlackout);
            interval.ProcessResponse(ResponseQuality.CorrectPerfect);
            interval.ProcessResponse(ResponseQuality.CorrectDifficult);

            interval.Interval.Should().Be(1);
        }
    }
}
