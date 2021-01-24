using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Clubs.Commands.DeleteClub
{
    public class DeleteClubCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteClubCommandHandler : IRequestHandler<DeleteClubCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteClubCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteClubCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Clubs.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Club), request.Id);
            }

            context.Clubs.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
