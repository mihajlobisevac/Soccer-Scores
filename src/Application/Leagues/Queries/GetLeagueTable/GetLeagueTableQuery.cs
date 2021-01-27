using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Application.Leagues.Queries.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Leagues.Queries.GetLeagueTable
{
    public class GetLeagueTableQuery : IRequest<LeagueTable>
    {
        public int SeasonId { get; set; }
    }

    public class GetLeagueTableQueryHandler : IRequestHandler<GetLeagueTableQuery, LeagueTable>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetLeagueTableQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<LeagueTable> Handle(GetLeagueTableQuery request, CancellationToken cancellationToken)
        {
            var matches = await context.Matches
                .Where(x => x.Season.Id == request.SeasonId)
                .Where(x => x.Incidents.Any(y => y.Class == IncidentClass.FT))
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.Incidents.Where(y => y.Class == IncidentClass.FT))
                .ProjectTo<MatchDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new LeagueTable(matches.GetClubs());
        }
    }
}
