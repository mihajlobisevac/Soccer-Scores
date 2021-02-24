using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
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

    public class ClubInTable : IMapFrom<Club>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Played { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int Points { get; set; }
    }
}
