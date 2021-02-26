using System.Collections.Generic;

namespace SoccerScores.Application.Leagues.Queries.GetLeagueTable
{
    public class LeagueTable
    {
        public LeagueTable(IEnumerable<ClubInTable> clubs) 
        {
            Clubs = clubs;
        }

        public IEnumerable<ClubInTable> Clubs { get; private set; }
    }
}
