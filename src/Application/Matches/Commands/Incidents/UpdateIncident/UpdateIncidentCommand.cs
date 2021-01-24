using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Commands.Incidents.UpdateIncident
{
    public class UpdateIncidentCommand : IRequest
    {
        public int Id { get; set; }
        public IncidentDto Incident { get; set; }
    }

    public class UpdateIncidentCommandHandler : IRequestHandler<UpdateIncidentCommand>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public UpdateIncidentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateIncidentCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Incidents
                .Include(x => x.PrimaryPlayer)
                .Include(x => x.SecondaryPlayer)
                .FirstOrDefaultAsync(x => x.Id == request.Id)
                ?? throw new NotFoundException(nameof(Incident), request.Id);

            var updatedEntity = await HandleIncidentExceptions.Handle(request.Incident, context, mapper);

            entity.HomeScore = updatedEntity.HomeScore;
            entity.AwayScore = updatedEntity.AwayScore;
            entity.Minute = updatedEntity.Minute;
            entity.Type = updatedEntity.Type;
            entity.Class = updatedEntity.Class;
            entity.IsHome = updatedEntity.IsHome;
            entity.Match = updatedEntity.Match;
            entity.PrimaryPlayer = updatedEntity.PrimaryPlayer;
            entity.SecondaryPlayer = updatedEntity.SecondaryPlayer;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
