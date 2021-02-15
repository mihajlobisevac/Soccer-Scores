using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Players.Queries.GetPlayersByClub
{
    public class GetPlayersByClubQuery : IRequest<IEnumerable<PlayerDto>>
    {
        public int ClubId { get; set; }
    }

    public class GetPlayersByClubQueryHandler : IRequestHandler<GetPlayersByClubQuery, IEnumerable<PlayerDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetPlayersByClubQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<PlayerDto>> Handle(GetPlayersByClubQuery request, CancellationToken cancellationToken)
        {
            return await context.ClubPlayers
                .Include(x => x.Club)
                .Where(x => x.Club.Id == request.ClubId)
                .ProjectTo<PlayerDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
