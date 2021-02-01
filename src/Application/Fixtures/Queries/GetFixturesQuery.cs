using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Fixtures.Queries
{
    public class GetFixturesQuery : IRequest<IEnumerable<CompetitionVm>>
    {
        public string Date { get; set; } = "2017/10/5"; //datetime.now
    }

    public class GetFixturesQueryHandler : IRequestHandler<GetFixturesQuery, IEnumerable<CompetitionVm>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetFixturesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CompetitionVm>> Handle(GetFixturesQuery request, CancellationToken cancellationToken)
        {
            var matches = await context.Matches
                .Where(x => x.KickOff.Date == DateTime.Parse(request.Date).Date)
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.Season).ThenInclude(y => y.Competition).ThenInclude(z => z.Country)
                .Include(x => x.Incidents.Where(y => y.Class == IncidentClass.FT))
                .ToListAsync(cancellationToken);

            var groupedMatches = matches
                .GroupBy(x => x.Season.Competition.Id)
                .Select(group => group.ToList())
                .ToList();

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
