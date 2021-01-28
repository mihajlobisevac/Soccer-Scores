using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Players.Commands.DeletePlayer
{
    public class DeletePlayerCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand>
    {
        private readonly IApplicationDbContext context;

        public DeletePlayerCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            var entity = await GetPlayer(request.Id);

            await RemoveClubPlayer(request);

            context.Players.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task RemoveClubPlayer(DeletePlayerCommand request)
        {
            var clubPlayer = await context.ClubPlayers.FirstOrDefaultAsync(x => x.Player.Id == request.Id);

            if (clubPlayer is not null)
            {
                context.ClubPlayers.Remove(clubPlayer);
            }
        }

        private async Task<Player> GetPlayer(int id)
        {
            var player = await context.Players.FindAsync(id);

            if (player is null)
            {
                throw new NotFoundException(nameof(Player), id);
            }

            return player;
        }
    }
}
