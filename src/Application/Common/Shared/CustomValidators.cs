using System;

namespace SoccerScores.Application.Common.Shared
{
    public static class CustomValidators
    {
        public static bool BeAValidDate(string value)
        {
            return DateTime.TryParse(value, out _);
        }
    }
}
