using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Application.Matches.Queries.GetMatchesByClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Queries.GetMatchesByClub
{
    public class GetMatchesByClubQuery : IRequest<ICollection<MatchByClubDto>>
    {
        public int ClubId { get; set; }
    }

    public class GetMatchesByClubQueryHandler : IRequestHandler<GetMatchesByClubQuery, ICollection<MatchByClubDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetMatchesByClubQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ICollection<MatchByClubDto>> Handle(GetMatchesByClubQuery request, CancellationToken cancellationToken)
        {
            var matches = await context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.Season).ThenInclude(y => y.Competition)
                .Where(x => x.KickOff < DateTime.Now)
                .Where(x => x.HomeTeam.Id == request.ClubId || x.AwayTeam.Id == request.ClubId)
                .Take(5)
                .ToListAsync(cancellationToken);


            var futureMatches = await context.Matches
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.Season).ThenInclude(y => y.Competition)
                .Where(x => x.KickOff >= DateTime.Now)
                .Where(x => x.HomeTeam.Id == request.ClubId || x.AwayTeam.Id == request.ClubId)
                .Take(5)
                .ToListAsync(cancellationToken);

            matches.AddRange(futureMatches);

            return mapper.Map<ICollection<MatchByClubDto>>(matches.OrderBy(x => x.KickOff));
        }
    }
}
