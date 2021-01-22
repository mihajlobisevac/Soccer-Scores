using Domain.Enums;
using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Matches.Commands.UpdateIncident
{
    public class UpdateIncidentCommand : IRequest
    {
        public int Id { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int Minute { get; set; }
        public string Type { get; set; }
        public string Class { get; set; }
        public bool IsHome { get; set; }
        public int MatchId { get; set; }
        public int? PrimaryPlayerId { get; set; }
        public int? SecondaryPlayerId { get; set; }
    }

    public class UpdateIncidentCommandHandler : IRequestHandler<UpdateIncidentCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateIncidentCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateIncidentCommand request, CancellationToken cancellationToken)
        {
            var incident = await context.Incidents.FindAsync(request.Id)
                ?? throw new NotFoundException(nameof(City), request.Id);

            incident.HomeScore = request.HomeScore;
            incident.AwayScore = request.AwayScore;
            incident.Minute = request.Minute;
            incident.Type = (IncidentType) Enum.Parse(typeof(IncidentType), request.Type, false);
            incident.Class = (IncidentClass) Enum.Parse(typeof(IncidentClass), request.Class, false);
            incident.IsHome = request.IsHome;

            incident.Match = await context.Matches.FindAsync(request.MatchId)
                    ?? throw new NotFoundException(nameof(Match), request.MatchId);

            incident.PrimaryPlayer = null;
            incident.SecondaryPlayer = null;

            if (request.PrimaryPlayerId != null)
            {
                incident.PrimaryPlayer = await context.Players.FindAsync(request.PrimaryPlayerId)
                ?? throw new NotFoundException(nameof(Player), request.MatchId);
            }

            if (request.SecondaryPlayerId != null)
            {
                incident.SecondaryPlayer = await context.Players.FindAsync(request.SecondaryPlayerId)
                    ?? throw new NotFoundException(nameof(Player), request.MatchId);
            }

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
