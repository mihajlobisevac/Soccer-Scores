using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Fixtures.Queries
{
    public class GetFixturesQuery : IRequest<IEnumerable<SeasonWithMatches>>
    {
        public string Date { get; set; } = "2017/10/5"; //datetime.now
    }

    public class GetFixturesQueryHandler : IRequestHandler<GetFixturesQuery, IEnumerable<SeasonWithMatches>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetFixturesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<SeasonWithMatches>> Handle(GetFixturesQuery request, CancellationToken cancellationToken)
        {
            var matches = await GetMatches(request);

            return matches
                .GroupByCompetition()
                .ToSeasonsWithMatches(mapper);
        }

        private async Task<IEnumerable<Match>> GetMatches(GetFixturesQuery request)
            => await context.Matches
                .Where(x => x.KickOff.Date == DateTime.Parse(request.Date).Date)
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.Season).ThenInclude(y => y.Competition).ThenInclude(z => z.Country)
                .Include(x => x.Incidents.Where(y => y.Class == IncidentClass.FT)).ThenInclude(z => z.PrimaryPlayer)
                .Include(x => x.Incidents.Where(y => y.Class == IncidentClass.FT)).ThenInclude(z => z.SecondaryPlayer)
                .ToListAsync();
    }
}
