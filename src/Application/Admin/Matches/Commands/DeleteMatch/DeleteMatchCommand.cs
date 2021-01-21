using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Matches.Commands.DeleteMatch
{
    public class DeleteMatchCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteMatchCommandHandler : IRequestHandler<DeleteMatchCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteMatchCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteMatchCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Matches.FindAsync(request.Id)
                ?? throw new NotFoundException(nameof(Match), request.Id);

            context.Matches.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
