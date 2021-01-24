using AutoMapper;
using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Commands.CreateMatch
{
    public class CreateMatchCommand : IRequest<int>
    {
        public string KickOff { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int SeasonId { get; set; }
    }

    public class CreateMatchCommandHandler : IRequestHandler<CreateMatchCommand, int>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public CreateMatchCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            var homeTeam = await context.Clubs.FindAsync(request.HomeTeamId)
                ?? throw new NotFoundException(nameof(Club), request.HomeTeamId);

            var awayTeam = await context.Clubs.FindAsync(request.AwayTeamId)
                ?? throw new NotFoundException(nameof(Club), request.AwayTeamId);

            var season = await context.Seasons.FindAsync(request.SeasonId)
                ?? throw new NotFoundException(nameof(Season), request.SeasonId);

            var entity = new Match
            {
                KickOff = DateTime.Parse(request.KickOff),
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
                Season = season,
            };

            context.Matches.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
