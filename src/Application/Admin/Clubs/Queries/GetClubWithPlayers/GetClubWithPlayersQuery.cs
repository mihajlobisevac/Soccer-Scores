using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Clubs.Queries.GetClubWithPlayers
{
    public class GetClubWithPlayersQuery : IRequest<ClubWithPlayersDto>
    {
        public int Id { get; set; }
    }

    public class GetClubWithPlayersQueryHandler : IRequestHandler<GetClubWithPlayersQuery, ClubWithPlayersDto>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetClubWithPlayersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ClubWithPlayersDto> Handle(GetClubWithPlayersQuery request, CancellationToken cancellationToken)
        {
            var club = await context.Clubs
                .Include(x => x.City)
                .ThenInclude(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            var players = await context.ClubPlayers
                .Where(x => x.Club == club)
                .ProjectTo<ClubPlayerDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            var clubWithPlayers = mapper.Map<ClubWithPlayersDto>(club);

            clubWithPlayers.Players = players;

            return clubWithPlayers;
        }
    }
}
