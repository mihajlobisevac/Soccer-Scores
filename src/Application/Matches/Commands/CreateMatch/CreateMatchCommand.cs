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
        public int GameWeek { get; set; }
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
            var entity = new Match
            {
                KickOff = DateTime.Parse(request.KickOff),
                GameWeek = request.GameWeek,
                HomeTeam = await GetClub(request.HomeTeamId),
                AwayTeam = await GetClub(request.AwayTeamId),
                Season = await GetSeason(request.SeasonId),
            };

            context.Matches.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        private async Task<Club> GetClub(int id)
        {
            var club = await context.Clubs.FindAsync(id);

            if (club is null)
            {
                throw new NotFoundException(nameof(Club), id);
            }

            return club;
        }

        private async Task<Season> GetSeason(int id)
        {
            var season = await context.Seasons.FindAsync(id);

            if (season is null)
            {
                throw new NotFoundException(nameof(Season), id);
            }

            return season;
        }
    }
}
