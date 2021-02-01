using AutoMapper;
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
                .Include(x => x.Season).ThenInclude(y => y.Competition)
                .ToListAsync(cancellationToken);

            var compsWithMatches = matches
                .GroupBy(x => x.Season.Competition.Id)
                .Select(group => group.ToList())
                .ToList();

            var entities = new List<CompetitionVm>();
            int uniqueCompetition = -1;

            for (int i = 0; i < compsWithMatches.Count; i++)
            {
                foreach (var match in compsWithMatches[i])
                {
                    if (uniqueCompetition != match.Season.Competition.Id)
                    {
                        uniqueCompetition = match.Season.Competition.Id;

                        var compWithMatches = new CompetitionVm
                        {
                            Id = match.Season.Id,
                            Name = match.Season.Competition.Name,
                            Matches = new List<MatchViewModel> { mapper.Map<MatchViewModel>(matches[i]) }
                        };

                        entities.Add(compWithMatches);
                    }
                    else
                    {
                        entities[i].Matches.Add(mapper.Map<MatchViewModel>(matches[i]));
                    }
                }
            }

            return entities;
        }
    }
}
