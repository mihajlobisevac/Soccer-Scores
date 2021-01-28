using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Players.Queries.GetPlayer
{
    public class GetPlayerQuery : IRequest<PlayerDto>
    {
        public int Id { get; set; }
    }

    public class GetPlayerQueryHandler : IRequestHandler<GetPlayerQuery, PlayerDto>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetPlayerQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PlayerDto> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
        {
            bool playerHasClub = context.ClubPlayers.Any(x => x.Player.Id == request.Id);

            if (playerHasClub)
            {
                var playerWithClub = await context.ClubPlayers
                    .Include(x => x.Club)
                    .Include(x => x.Player).ThenInclude(y => y.Nationality)
                    .Include(x => x.Player).ThenInclude(y => y.PlaceOfBirth)
                    .FirstOrDefaultAsync(x => x.Player.Id == request.Id);

                return mapper.Map<PlayerDto>(playerWithClub);
            }

            var player = await context.Players
                .Include(x => x.Nationality)
                .Include(x => x.PlaceOfBirth)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return mapper.Map<PlayerDto>(player);
        }
    }
}
