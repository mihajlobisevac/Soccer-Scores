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
            var entity = await GetIncident(request.Id);

            context.Incidents.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<Incident> GetIncident(int id)
        {
            var incident = await context.Incidents.FindAsync(id);

            if (incident is null)
            {
                throw new NotFoundException(nameof(Incident), id);
            }

            return incident;
        }
    }
}
