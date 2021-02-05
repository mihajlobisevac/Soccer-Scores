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

        internal static IEnumerable<CompetitionVm> ToCompetitionVm(this List<List<Match>> groupedMatches, IMapper mapper)
        {
            var compList = new List<CompetitionVm>();
            int uniqueCompetition = -1;

            for (int i = 0; i < groupedMatches.Count; i++)
            {
                foreach (var match in groupedMatches[i])
                {
                    if (uniqueCompetition != match.Season.Competition.Id)
                    {
                        uniqueCompetition = match.Season.Competition.Id;

                        compList.Add(new CompetitionVm
                        {
                            Id = match.Season.Id,
                            Name = match.Season.Competition.Name,
                            Flag = match.Season.Competition.Country.Flag,
                            Matches = new List<MatchViewModel>()
                        });
                    }

                    compList[i].Matches.Add(mapper.Map<MatchViewModel>(match));
                }
            }

            return compList;
        }
    }
}
