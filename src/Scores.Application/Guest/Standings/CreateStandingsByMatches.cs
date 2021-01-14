using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scores.Application.Guest.Standings
{
    [Service]
    public class CreateStandingsByMatches
    {
        public class ClubViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Played { get; set; }
            public int Wins { get; set; }
            public int Draws { get; set; }
            public int Losses { get; set; }
            public int Points { get; set; }
        }

        public IEnumerable<ClubViewModel> Do(List<GetMatchById.Response> Matches)
        {
            var StandingsTable = new List<ClubViewModel>();

            foreach (var match in Matches)
            {
                if (HomeTeamNotInTable(StandingsTable, match.HomeTeam.Id))
                {
                    var newClub = CalculateClub(match.HomeTeam.Id, match.HomeTeam.Name, Matches);

                    StandingsTable.Add(newClub);
                }

                if (AwayTeamNotInTable(StandingsTable, match.AwayTeam.Id))
                {
                    var newClub = CalculateClub(match.AwayTeam.Id, match.AwayTeam.Name, Matches);

                    StandingsTable.Add(newClub);
                }
            }

            return StandingsTable;
        }

        private ClubViewModel CalculateClub(int clubId, string name, List<GetMatchById.Response> matches)
        {
            var club = new ClubViewModel
            {
                Id = clubId,
                Name = name,
                Played = CalculatePlayedMatches(clubId, matches),
                Wins = CalculateWins(clubId, matches),
                Draws = CalculateDraws(clubId, matches),
                Losses = CalculateLosses(clubId, matches),
                Points = 0,
            };

            club.Points = CalculatePoints(club.Wins, club.Draws);

            return club;
        }

        private int CalculatePlayedMatches(int clubId, List<GetMatchById.Response> matches)
        {
            return matches
                .FindAll(x => x.HomeTeam.Id == clubId || x.AwayTeam.Id == clubId)
                .Count;
        }

        private int CalculateWins(int clubId, List<GetMatchById.Response> matches)
        {
            return
                matches.FindAll(x => clubId == x.HomeTeam.Id &&
                    x.Incidents.Any(y => y.Class == "FT" && y.HomeScore > y.AwayScore)).Count
                +
                matches.FindAll(x => clubId == x.AwayTeam.Id &&
                    x.Incidents.Any(y => y.Class == "FT" && y.HomeScore < y.AwayScore)).Count;
        }

        private int CalculateDraws(int clubId, List<GetMatchById.Response> matches)
        {
            return
                matches.FindAll(x => (clubId == x.HomeTeam.Id || clubId == x.AwayTeam.Id) &&
                    x.Incidents.Any(y => y.Class == "FT" && y.HomeScore == y.AwayScore)).Count;
        }

        private int CalculateLosses(int clubId, List<GetMatchById.Response> matches)
        {
            return
                matches.FindAll(x => clubId == x.HomeTeam.Id &&
                    x.Incidents.Any(y => y.Class == "FT" && y.HomeScore < y.AwayScore)).Count
                +
                matches.FindAll(x => clubId == x.AwayTeam.Id &&
                    x.Incidents.Any(y => y.Class == "FT" && y.HomeScore > y.AwayScore)).Count;
        }

        private int CalculatePoints(int clubWins, int clubDraws) => (3 * clubWins) + clubDraws;
        private bool AwayTeamNotInTable(List<ClubViewModel> StandingsTable, int clubId) => !StandingsTable.Any(x => x.Id == clubId);
        private bool HomeTeamNotInTable(List<ClubViewModel> StandingsTable, int clubId) => !StandingsTable.Any(x => x.Id == clubId);
    }
}
 