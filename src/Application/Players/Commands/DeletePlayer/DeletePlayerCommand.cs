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
            var entity = await context.Players.FindAsync(request.Id) 
                ?? throw new NotFoundException(nameof(Player), request.Id);

            var clubPlayer = await context.ClubPlayers.FirstOrDefaultAsync(x => x.Player.Id == request.Id);

            if (clubPlayer != null)
            {
                context.ClubPlayers.Remove(clubPlayer);
            }

            context.Players.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
