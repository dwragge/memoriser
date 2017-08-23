using System;

namespace Memoriser.ApplicationCore.Models
{
    public class RepetitionInterval
    {
        private float _easinessFactor = 2.5f;
        private int _interval;

        public int Interval => CalculateIntervalInternal(_interval);

        private int CalculateIntervalInternal(int i)
        {
            if (i == 1) return 1;
            if (i == 2) return 6;
            
            float previousInterval = CalculateIntervalInternal(i - 1) * _easinessFactor;
            return (int) Math.Ceiling(previousInterval);
        }

        public void RecalculateEF(ResponseQuality responseQuality)
        {
            if (responseQuality < ResponseQuality.CorrectDifficult)
            {
                _interval = 1;
            }

            int q = (int)responseQuality;
            float updated = _easinessFactor - 0.8f + 0.28f * q - 0.02f * q * q;
            if (updated < 1.3f)
            {
                updated = 1.3f;
            }

            _easinessFactor = updated;
        }

        private RepetitionInterval(int interval, float ef)
        {
            _interval = interval;
            _easinessFactor = ef;
        }

        public static RepetitionInterval FromValues(int interval, float ef)
        {
            return new RepetitionInterval(interval, ef);
        }
    }
}
