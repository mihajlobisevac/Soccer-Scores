using Domain.Enums;
using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Matches.Commands.CreateIncidents
{
    public class BatchCreateIncidentsCommand : IRequest<int[]>
    {
        public List<IncidentVm> Incidents { get; set; }
    }

    public class BatchCreateIncidentsCommandHandler : IRequestHandler<BatchCreateIncidentsCommand, int[]>
    {
        private readonly IApplicationDbContext context;

        public BatchCreateIncidentsCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int[]> Handle(BatchCreateIncidentsCommand request, CancellationToken cancellationToken)
        {
            var incidents = await HandleIncidents(request.Incidents, context);

            context.Incidents.AddRange(incidents);

            await context.SaveChangesAsync(cancellationToken);

            var entityIds = new List<int>();

            incidents.ForEach(i => entityIds.Add(i.Id));

            return entityIds.ToArray();
        }

        private static async Task<List<Incident>> HandleIncidents(List<IncidentVm> incidents, IApplicationDbContext context)
        {
            List<Incident> Incidents = new List<Incident>();

            foreach (var i in incidents)
            {
                var incidentToAdd = new Incident
                {
                    HomeScore = i.HomeScore,
                    AwayScore = i.AwayScore,
                    Minute = i.Minute,
                    Type = (IncidentType) Enum.Parse(typeof(IncidentType), i.Type, false),
                    Class = (IncidentClass) Enum.Parse(typeof(IncidentClass), i.Class, false),
                    IsHome = i.IsHome,
                };

                incidentToAdd.Match = await context.Matches.FindAsync(i.MatchId)
                    ?? throw new NotFoundException(nameof(Match), i.MatchId);

                if (i.PrimaryPlayerId != null)
                {
                    incidentToAdd.PrimaryPlayer = await context.Players.FindAsync(i.PrimaryPlayerId)
                    ?? throw new NotFoundException(nameof(Player), i.MatchId);
                }

                if (i.SecondaryPlayerId != null)
                {
                    incidentToAdd.SecondaryPlayer = await context.Players.FindAsync(i.SecondaryPlayerId)
                        ?? throw new NotFoundException(nameof(Player), i.MatchId);
                }

                Incidents.Add(incidentToAdd);
            }

            return Incidents;
        }
    }
}
