using AutoMapper;
using SoccerScores.Application.Fixtures.Queries;
using SoccerScores.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SoccerScores.Application.Fixtures
{
    internal static class FixturesExtensions
    {
        internal static List<List<Match>> GroupByCompetition(this IEnumerable<Match> matches)
            => matches.GroupBy(x => x.Season.Competition.Id)
                .Select(group => group.ToList())
                .ToList();

        internal static IEnumerable<SeasonWithMatches> ToSeasonsWithMatches(this List<List<Match>> groupedMatches, IMapper mapper)
        {
            int COMPETITION_ID = -1;

            var seasons = new List<SeasonWithMatches>();

            for (int i = 0; i < groupedMatches.Count; i++)
            {
                foreach (var match in groupedMatches[i])
                {
                    if (COMPETITION_ID.IsNotEqual(match.Season.Competition.Id))
                    {
                        COMPETITION_ID = match.Season.Competition.Id;
                        seasons.Add(mapper.Map<SeasonWithMatches>(match.Season));
                    }

                    seasons[i].Matches.Add(mapper.Map<MatchViewModel>(match));
                }
            }

            return seasons;
        }

        private static bool IsNotEqual(this int id, int newId) => id != newId;
    }
}
