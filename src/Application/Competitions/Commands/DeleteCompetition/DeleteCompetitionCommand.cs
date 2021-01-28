using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Competitions.Commands.DeleteCompetition
{
    public class DeleteCompetitionCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteCompetitionCommandHandler : IRequestHandler<DeleteCompetitionCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteCompetitionCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteCompetitionCommand request, CancellationToken cancellationToken)
        {
            var entity = await GetCompetition(request.Id);

            context.Competitions.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<Competition> GetCompetition(int id)
        {
            var competition = await context.Competitions.FindAsync(id);

            if (competition is null)
            {
                throw new NotFoundException(nameof(Competition), id);
            }

            return competition;
        }
    }
}
