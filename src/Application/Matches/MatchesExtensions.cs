using System;

namespace SoccerScores.Application.Matches
{
    internal static class MatchesExtensions
    {
        internal static bool GetUpcomingOrNot(this bool isFuture, DateTime kickOff)
        {
            return isFuture ? DateTime.Now >= kickOff : DateTime.Now < kickOff;
        }
    }
}
