using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Players.Commands.RemoveFromClub
{
    public class RemoveFromClubCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class RemoveFromClubCommandHandler : IRequestHandler<RemoveFromClubCommand>
    {
        private readonly IApplicationDbContext context;

        public RemoveFromClubCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(RemoveFromClubCommand request, CancellationToken cancellationToken)
        {
            var entity = await GetClubPlayer(request.Id);

            context.ClubPlayers.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<ClubPlayer> GetClubPlayer(int playerId)
        {
            var clubPlayer = await context.ClubPlayers.FirstOrDefaultAsync(x => x.Player.Id == playerId);

            if (clubPlayer is null)
            {
                throw new NotFoundException(nameof(ClubPlayer), playerId);
            }

            return clubPlayer;
        }
    }
}
