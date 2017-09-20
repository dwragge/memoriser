using System;

namespace Memoriser.ApplicationCore.LearningItems
{
    public class RepetitionInterval
    {
        private int _interval = -2;

        public int Interval => CalculateIntervalInternal(_interval);
        public float EasinessFactor { get; private set; } = 2.5f;

        public Guid Id { get; set; }

        private int CalculateIntervalInternal(int i)
        {
            if (i <= 0) return 0;
            if (i == 1) return 1;
            if (i == 2) return 6;
            
            float previousInterval = CalculateIntervalInternal(i - 1) * EasinessFactor;
            return (int) Math.Ceiling(previousInterval);
        }

        private RepetitionInterval()
        {
        }

        public void ProcessResponse(ResponseQuality responseQuality)
        {
            TryIncrementInterval(responseQuality);
            EasinessFactor = CalculateUpdatedEasinessFactor(responseQuality);
        }

        private void TryIncrementInterval(ResponseQuality responseQuality)
        {
            if (responseQuality < ResponseQuality.CorrectDifficult)
            {
                if (_interval < 0)
                {
                    return;
                }

                _interval = 1;
            }
            else
            {
                _interval++;
            }
        }

        private float CalculateUpdatedEasinessFactor(ResponseQuality responseQuality)
        {
            int q = (int)responseQuality;
            float updated = EasinessFactor - 0.8f + 0.28f * q - 0.02f * q * q;
            if (updated < 1.3f)
            {
                updated = 1.3f;
            }
            return updated;
        }

        private RepetitionInterval(int interval, float ef)
        {
            _interval = interval;
            EasinessFactor = ef;
        }

        public static RepetitionInterval FromValues(int interval, float ef)
        {
            return new RepetitionInterval(interval, ef);
        }

        public static RepetitionInterval NewDefault()
        {
            return new RepetitionInterval();
        }
    }
}
