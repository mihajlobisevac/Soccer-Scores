using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Commands.Players.UpdateMatchPlayer
{
    public class UpdateMatchPlayerCommand : IRequest
    {
        public int Id { get; set; }
        public bool IsHome { get; set; }
        public bool IsSubstitute { get; set; }
        public int ShirtNumber { get; set; }

        public int PlayerId { get; set; }
    }

    public class UpdateMatchPlayerCommandHandler : IRequestHandler<UpdateMatchPlayerCommand>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public UpdateMatchPlayerCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateMatchPlayerCommand request, CancellationToken cancellationToken)
        {
            var matchPlayer = await GetMatchPlayer(request.Id);

            matchPlayer.IsHome = request.IsHome;
            matchPlayer.IsSubstitute = request.IsSubstitute;
            matchPlayer.Player = await GetPlayer(request.PlayerId);
            matchPlayer.ShirtNumber = request.ShirtNumber;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<MatchPlayer> GetMatchPlayer(int id)
        {
            var matchPlayer = await context.MatchPlayers
                .Include(x => x.Player)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (matchPlayer is null)
            {
                throw new NotFoundException(nameof(MatchPlayer), id);
            }

            return matchPlayer;
        }

        private async Task<Player> GetPlayer(int id)
        {
            var match = await context.Players.FindAsync(id);

            if (match is null)
            {
                throw new NotFoundException(nameof(Player), id);
            }

            return match;
        }
    }
}
