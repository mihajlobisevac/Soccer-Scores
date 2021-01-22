using AutoMapper;
using Domain.Enums;
using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Matches.Commands.Incidents.CreateIncidents
{
    public class BatchCreateIncidentsCommand : IRequest<int[]>
    {
        public List<IncidentDto> Incidents { get; set; }
    }

    public class BatchCreateIncidentsCommandHandler : IRequestHandler<BatchCreateIncidentsCommand, int[]>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public BatchCreateIncidentsCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<int[]> Handle(BatchCreateIncidentsCommand request, CancellationToken cancellationToken)
        {
            var incidents = await CreateIncidentList(request.Incidents, context, mapper);

            context.Incidents.AddRange(incidents);

            await context.SaveChangesAsync(cancellationToken);

            return incidents
                .Select(x => x.Id)
                .ToArray();
        }

        private static async Task<List<Incident>> CreateIncidentList(
            List<IncidentDto> incidents, 
            IApplicationDbContext context,
            IMapper mapper)
        {
            List<Incident> Incidents = new List<Incident>();

            foreach (var i in incidents)
            {
                var incidentToAdd = await HandleIncidentExceptions.Handle(i, context, mapper);

                Incidents.Add(incidentToAdd);
            }

            return Incidents;
        }
    }
}
