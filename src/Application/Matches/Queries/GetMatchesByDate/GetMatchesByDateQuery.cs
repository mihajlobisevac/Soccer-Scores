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
    public class GetMatchesByDateQuery : IRequest<ICollection<MatchByDateDto>>
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }

    public class GetMatchesByDateQueryHandler : IRequestHandler<GetMatchesByDateQuery, ICollection<MatchByDateDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetMatchesByDateQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ICollection<MatchByDateDto>> Handle(GetMatchesByDateQuery request, CancellationToken cancellationToken)
        {
            var date = new DateTime(request.Year, request.Month, request.Day);

            var entity = await context.Matches
                .Where(x => x.KickOff == date)
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.Season).ThenInclude(y => y.Competition)
                .ToListAsync(cancellationToken);

            return mapper.Map<ICollection<MatchByDateDto>>(entity);
        }
    }
}
