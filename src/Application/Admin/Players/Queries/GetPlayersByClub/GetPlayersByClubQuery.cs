using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static SoccerScores.Application.Admin.Players.Queries.GetPlayersByClub.PlayersVm;

namespace SoccerScores.Application.Admin.Players.Queries.GetPlayersByClub
{
    public class GetPlayersByClubQuery : IRequest<PlayersVm>
    {
        public int Id { get; set; }
    }

    public class GetPlayersByClubQueryHandler : IRequestHandler<GetPlayersByClubQuery, PlayersVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetPlayersByClubQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PlayersVm> Handle(GetPlayersByClubQuery request, CancellationToken cancellationToken)
        {
            var players = await context.ClubPlayers
                .Include(x => x.Club)
                .Where(x => x.Club.Id == request.Id)
                .ProjectTo<PlayerViewModel>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PlayersVm { Players = players };
        }
    }
}
