using System;
using System.Collections.Generic;
using System.Text;

namespace Memoriser.ApplicationCore.Models
{
    public enum ResponseQuality
    {
        IncorrectBlackout = 0,
        IncorrectRemembered = 1,
        IncorrectEasyRecalled = 2,
        CorrectDifficult = 3,
        CorrectHesitate = 4,
        CorrectPerfect = 5
    }
}
