using Domain.Enums;
using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Matches.Commands.CreateIncidents
{
    public class CreateIncidentCommand : IRequest<int>
    {
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

    public class CreateIncidentCommandHandler : IRequestHandler<CreateIncidentCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateIncidentCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Incident
            {
                HomeScore = request.HomeScore,
                AwayScore = request.AwayScore,
                Minute = request.AwayScore,
                Type = (IncidentType) Enum.Parse(typeof(IncidentType), request.Type, false),
                Class = (IncidentClass) Enum.Parse(typeof(IncidentClass), request.Class, false),
            };

            entity.Match = await context.Matches.FindAsync(request.MatchId)
                    ?? throw new NotFoundException(nameof(Match), request.MatchId);

            if (request.PrimaryPlayerId != null)
            {
                entity.PrimaryPlayer = await context.Players.FindAsync(request.PrimaryPlayerId)
                ?? throw new NotFoundException(nameof(Player), request.MatchId);
            }

            if (request.SecondaryPlayerId != null)
            {
                entity.SecondaryPlayer = await context.Players.FindAsync(request.SecondaryPlayerId)
                    ?? throw new NotFoundException(nameof(Player), request.MatchId);
            }

            context.Incidents.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
