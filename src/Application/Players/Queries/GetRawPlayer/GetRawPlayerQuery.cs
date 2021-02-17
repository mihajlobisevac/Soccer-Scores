using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Players.Queries.GetRawPlayer
{
    public class GetRawPlayerQuery : IRequest<RawPlayerDto>
    {
        public int Id { get; set; }
    }

    public class GetRawPlayerQueryHandler : IRequestHandler<GetRawPlayerQuery, RawPlayerDto>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetRawPlayerQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<RawPlayerDto> Handle(GetRawPlayerQuery request, CancellationToken cancellationToken)
        {
            bool playerHasClub = context.ClubPlayers.Any(x => x.Player.Id == request.Id);

            if (playerHasClub)
            {
                return await GetClubPlayer(request.Id);
            }

            return await GetPlayer(request.Id);
        }

        private async Task<RawPlayerDto> GetClubPlayer(int id)
        {
            var playerWithClub = await context.ClubPlayers
                .Include(x => x.Club).ThenInclude(y => y.City).ThenInclude(z => z.Country)
                .Include(x => x.Player).ThenInclude(y => y.Nationality)
                .Include(x => x.Player).ThenInclude(y => y.PlaceOfBirth).ThenInclude(z => z.Country)
                .FirstOrDefaultAsync(x => x.Player.Id == id);

            if (playerWithClub is null)
            {
                throw new NotFoundException(nameof(ClubPlayer), id);
            }

            return mapper.Map<RawPlayerDto>(playerWithClub);
        }

        private async Task<RawPlayerDto> GetPlayer(int id)
        {
            var player = await context.Players
                .Include(x => x.Nationality)
                .Include(x => x.PlaceOfBirth).ThenInclude(y => y.Country)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (player is null)
            {
                throw new NotFoundException(nameof(Player), id);
            }

            return mapper.Map<RawPlayerDto>(player);
        }
    }
}
