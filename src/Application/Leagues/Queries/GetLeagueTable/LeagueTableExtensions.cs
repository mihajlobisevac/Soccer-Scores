using System.Collections.Generic;
using System.Linq;

namespace SoccerScores.Application.Leagues.Queries.GetLeagueTable
{
    internal static class LeagueTableExtensions
    {
        internal static List<ClubInTable> GetClubs(this List<MatchDto> matches)
        {
            var clubs = new List<ClubInTable>();

            foreach (var match in matches)
            {
                if (clubs.HasNoClub(match.HomeTeam.Id))
                {
                    var newClub = matches.CalculateClub(match.HomeTeam);

                    clubs.Add(newClub);
                }

                if (clubs.HasNoClub(match.AwayTeam.Id))
                {
                    var newClub = matches.CalculateClub(match.AwayTeam);

                    clubs.Add(newClub);
                }
            }

            return clubs;
        }

        internal static ClubInTable CalculateClub(this List<MatchDto> matches, ClubVm club)
        {
            var clubInTable = new ClubInTable
            {
                Id = club.Id,
                Name = club.Name,
                Played = matches.PlayedMatches(club.Id),
                Wins = matches.Wins(club.Id),
                Draws = matches.Draws(club.Id),
                Losses = matches.Losses(club.Id),
                Points = 0,
            };

            clubInTable.Points = (3 * clubInTable.Wins) + clubInTable.Draws;

            return clubInTable;
        }

        internal static int PlayedMatches(this List<MatchDto> matches, int clubId)
        {
            return matches.FindAll(x => x.HasClub(clubId)).Count;
        }

        internal static int Wins(this List<MatchDto> matches, int clubId)
        {
            return matches.FindAll(x => x.WonHome(clubId)).Count +
                matches.FindAll(x => x.WonAway(clubId)).Count;
        }

        internal static int Draws(this List<MatchDto> matches, int clubId)
        {
            return matches.FindAll(x => x.Drew(clubId)).Count;
        }

        internal static int Losses(this List<MatchDto> matches, int clubId)
        {
            return matches.FindAll(x => x.LostHome(clubId)).Count +
                matches.FindAll(x => x.LostAway(clubId)).Count;
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
