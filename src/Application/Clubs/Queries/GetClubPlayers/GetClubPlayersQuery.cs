using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Clubs.Queries.GetClubPlayers
{
    public class GetClubPlayersQuery : IRequest<IEnumerable<ClubPlayerDto>>
    {
        public int ClubId { get; set; }
    }

    public class GetClubPlayersQueryHandler : IRequestHandler<GetClubPlayersQuery, IEnumerable<ClubPlayerDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetClubPlayersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ClubPlayerDto>> Handle(GetClubPlayersQuery request, CancellationToken cancellationToken)
        {
            var club = await GetClub(request);
            var players = await GetPlayers(club);

            return players;
        }

        private async Task<List<ClubPlayerDto>> GetPlayers(Club club)
            => await context.ClubPlayers
                .Where(x => x.Club == club)
                .ProjectTo<ClubPlayerDto>(mapper.ConfigurationProvider)
                .ToListAsync();

        private async Task<Club> GetClub(GetClubPlayersQuery request)
            => await context.Clubs
                .Include(x => x.City)
                .ThenInclude(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == request.ClubId);
    }
}
