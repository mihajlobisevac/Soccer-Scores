using Application.Common.Extensions;
using AutoMapper;
using Domain.Enums;
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
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int Minute { get; set; }
        public string Type { get; set; }
        public string Class { get; set; }
        public bool IsHome { get; set; }

        public int MatchId { get; set; }
        public int PrimaryPlayerId { get; set; }
        public int SecondaryPlayerId { get; set; }
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
            var incident = await GetIncident(request.Id);

            incident.HomeScore = request.HomeScore;
            incident.AwayScore = request.AwayScore;
            incident.Minute = request.Minute;
            incident.Type = request.Type.ToEnum<IncidentType>();
            incident.Class = request.Class.ToEnum<IncidentClass>();
            incident.IsHome = request.IsHome;

            incident.Match = await GetMatch(request.MatchId);

            incident.PrimaryPlayer = await GetMatchPlayer(request.PrimaryPlayerId);
            incident.SecondaryPlayer = await GetMatchPlayer(request.SecondaryPlayerId);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<Incident> GetIncident(int id)
        {
            var incident = await context.Incidents
                .Include(x => x.PrimaryPlayer)
                .Include(x => x.SecondaryPlayer)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (incident is null)
            {
                throw new NotFoundException(nameof(Incident), id);
            }

            return incident;
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

        private async Task<MatchPlayer> GetMatchPlayer(int id)
        {
            if (id < 1)
            {
                return null;
            }

            var matchPlayer = await context.MatchPlayers.FindAsync(id);

            if (matchPlayer is null)
            {
                throw new NotFoundException(nameof(MatchPlayer), id);
            }

            return matchPlayer;
        }
    }
}
