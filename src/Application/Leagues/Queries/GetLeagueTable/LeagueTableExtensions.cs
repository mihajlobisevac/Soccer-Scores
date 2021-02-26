using System.Collections.Generic;
using System.Linq;

namespace SoccerScores.Application.Leagues.Queries.GetLeagueTable
{
    internal static class LeagueTableExtensions
    {
        internal static List<ClubInTable> GetTableWithClubs(this List<MatchDto> matches)
        {
            var table = new List<ClubInTable>();

            foreach (var match in matches)
            {
                if (table.HasNoClub(match.HomeTeam.Id))
                {
                    var newClub = matches.CreateClubWithStats(match.HomeTeam);

                    table.Add(newClub);
                }

                if (table.HasNoClub(match.AwayTeam.Id))
                {
                    var newClub = matches.CreateClubWithStats(match.AwayTeam);

                    table.Add(newClub);
                }
            }

            return table;
        }

        private static ClubInTable CreateClubWithStats(this List<MatchDto> matches, ClubVm club)
        {
            var clubInTable = new ClubInTable(matches)
            {
                Id = club.Id,
                Name = club.Name
            };

            clubInTable.CalculateStats();

            return clubInTable;
        }

        internal static bool HasNoClub(this List<ClubInTable> table, int clubId) => !table.Any(x => x.Id == clubId);
        internal static bool HasClub(this MatchDto match, int clubId) => match.HomeTeam.Id == clubId || match.AwayTeam.Id == clubId;
        internal static bool WonHome(this MatchDto match, int clubId) => clubId == match.HomeTeam.Id && match.Result.HomeScore > match.Result.AwayScore;
        internal static bool WonAway(this MatchDto match, int clubId) => clubId == match.AwayTeam.Id && match.Result.HomeScore < match.Result.AwayScore;
        internal static bool LostHome(this MatchDto match, int clubId) => clubId == match.HomeTeam.Id && match.Result.HomeScore < match.Result.AwayScore;
        internal static bool LostAway(this MatchDto match, int clubId) => clubId == match.AwayTeam.Id && match.Result.HomeScore > match.Result.AwayScore;
        internal static bool Drew(this MatchDto match, int clubId) => match.HasClub(clubId) && match.Result.HomeScore == match.Result.AwayScore;
    }
}
