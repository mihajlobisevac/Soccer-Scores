using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Application.Matches.Queries.GetMatchesByDate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Queries.GetMatchesByDate
{
    public class GetMatchesByDateQuery : IRequest<IEnumerable<MatchByDateDto>>
    {
        public string Date { get; set; }
    }

    public class GetMatchesByDateQueryHandler : IRequestHandler<GetMatchesByDateQuery, IEnumerable<MatchByDateDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetMatchesByDateQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<MatchByDateDto>> Handle(GetMatchesByDateQuery request, CancellationToken cancellationToken)
        {
            var entities = await context.Matches
                .Where(x => x.KickOff.Date == DateTime.Parse(request.Date).Date)
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.Season).ThenInclude(y => y.Competition)
                .ToListAsync(cancellationToken);

            return mapper.Map<IEnumerable<MatchByDateDto>>(entities);
        }
    }
}
