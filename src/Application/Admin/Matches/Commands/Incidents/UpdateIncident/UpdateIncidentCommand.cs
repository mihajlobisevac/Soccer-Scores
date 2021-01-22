using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Matches.Commands.Incidents.UpdateIncident
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

            entity.HomeScore = request.Incident.HomeScore;
            entity.AwayScore = request.Incident.AwayScore;
            entity.Minute = request.Incident.Minute;
            entity.Type = (IncidentType)Enum.Parse(typeof(IncidentType), request.Incident.Type, false);
            entity.Class = (IncidentClass)Enum.Parse(typeof(IncidentClass), request.Incident.Class, false);
            entity.IsHome = request.Incident.IsHome;

            entity.Match = await context.Matches.FindAsync(request.Incident.MatchId)
                    ?? throw new NotFoundException(nameof(Match), request.Incident.MatchId);

            entity.PrimaryPlayer = null;
            entity.SecondaryPlayer = null;

            if (request.Incident.PrimaryPlayerId != null)
            {
                entity.PrimaryPlayer = await context.Players.FindAsync(request.Incident.PrimaryPlayerId)
                    ?? throw new NotFoundException(nameof(Player), request.Incident.PrimaryPlayerId);
            }

            if (request.Incident.SecondaryPlayerId != null)
            {
                entity.SecondaryPlayer = await context.Players.FindAsync(request.Incident.SecondaryPlayerId)
                    ?? throw new NotFoundException(nameof(Player), request.Incident.SecondaryPlayerId);
            }

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
