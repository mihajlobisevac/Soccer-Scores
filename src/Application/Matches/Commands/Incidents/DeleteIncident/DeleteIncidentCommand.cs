using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Commands.Incidents.DeleteIncident
{
    public class DeleteIncidentCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteIncidentCommandHandler : IRequestHandler<DeleteIncidentCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteIncidentCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteIncidentCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Incidents.FindAsync(request.Id)
                ?? throw new NotFoundException(nameof(Incident), request.Id);

            context.Incidents.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
