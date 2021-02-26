using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System.Collections.Generic;

namespace SoccerScores.Application.Leagues.Queries.GetLeagueTable
{
    public class ClubInTable : IMapFrom<Club>
    {
        public ClubInTable()
        {
        }

        public ClubInTable(List<MatchDto> matches)
        {
            Matches = matches;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Played { get; private set; } = 0;
        public int Wins { get; private set; } = 0;
        public int Draws { get; private set; } = 0;
        public int Losses { get; private set; } = 0;
        public int Points { get; private set; } = 0;
        public List<MatchDto> Matches { get; }

        internal void CalculateStats()
        {
            CalculatePlayedMatches();
            CalculateWins();
            CalculateDraws();
            CalculateLosses();
            CalcualtePoints();
        }

        private void CalculatePlayedMatches()
        {
            Played = Matches.FindAll(x => x.HasClub(Id)).Count;
        }

        private void CalculateWins()
        {
            Wins = Matches.FindAll(x => x.WonHome(Id)).Count +
                Matches.FindAll(x => x.WonAway(Id)).Count;
        }

        private void CalculateDraws()
        {
            Draws = Matches.FindAll(x => x.Drew(Id)).Count;
        }

        private void CalculateLosses()
        {
            Losses = Matches.FindAll(x => x.LostHome(Id)).Count +
                Matches.FindAll(x => x.LostAway(Id)).Count;
        }

        private void CalcualtePoints()
        {
            Points = (3 * Wins) + Draws;
        }
    }
}
