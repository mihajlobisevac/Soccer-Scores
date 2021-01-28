using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Commands.DeleteMatch
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
            var entity = await GetMatch(request.Id);

            context.Matches.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<Match> GetMatch(int id)
        {
            var match = await context.Matches.FindAsync(id);

            if (match is null)
            {
                throw new NotFoundException(nameof(Match), id);
            }

            return match;
        }
    }
}
