using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Seasons.Commands.DeleteSeason
{
    public class DeleteSeasonCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteSeasonCommandHandler : IRequestHandler<DeleteSeasonCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteSeasonCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteSeasonCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Seasons.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(City), request.Id);
            }

            context.Seasons.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
