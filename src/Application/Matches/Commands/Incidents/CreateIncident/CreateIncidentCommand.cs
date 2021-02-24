using Application.Common.Extensions;
using AutoMapper;
using Domain.Enums;
using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Commands.Incidents.CreateIncident
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
        public int PrimaryPlayerId { get; set; }
        public int SecondaryPlayerId { get; set; }
    }

    public class CreateIncidentCommandHandler : IRequestHandler<CreateIncidentCommand, int>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public CreateIncidentCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
        {
            var incident = new Incident
            {
                HomeScore = request.HomeScore,
                AwayScore = request.AwayScore,
                Minute = request.Minute,
                Type = request.Type.ToEnum<IncidentType>(),
                Class = request.Class.ToEnum<IncidentClass>(),
                IsHome = request.IsHome,
                Match = await GetMatch(request.MatchId),
                PrimaryPlayer = await GetMatchPlayer(request.PrimaryPlayerId),
                SecondaryPlayer = await GetMatchPlayer(request.SecondaryPlayerId),
            };

            context.Incidents.Add(incident);

            await context.SaveChangesAsync(cancellationToken);

            return incident.Id;
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
