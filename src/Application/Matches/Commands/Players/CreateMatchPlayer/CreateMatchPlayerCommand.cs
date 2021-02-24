using AutoMapper;
using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Commands.Players.CreateMatchPlayer
{
    public class CreateMatchPlayerCommand : IRequest<int>
    {
        public bool IsHome { get; set; }
        public bool IsSubstitute { get; set; }
        public int ShirtNumber { get; set; }

        public int PlayerId { get; set; }
        public int MatchId { get; set; }
    }

    public class CreateMatchPlayerCommandHandler : IRequestHandler<CreateMatchPlayerCommand, int>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public CreateMatchPlayerCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateMatchPlayerCommand request, CancellationToken cancellationToken)
        {
            var matchPlayer = new MatchPlayer
            {
                IsHome = request.IsHome,
                IsSubstitute = request.IsSubstitute,
                ShirtNumber = request.ShirtNumber,
                Player = await GetPlayer(request.PlayerId),
                Match = await GetMatch(request.MatchId),
            };

            context.MatchPlayers.Add(matchPlayer);

            await context.SaveChangesAsync(cancellationToken);

            return matchPlayer.Id;
        }

        private async Task<Player> GetPlayer(int id)
        {
            var match = await context.Players.FindAsync(id);

            if (match is null)
            {
                throw new NotFoundException(nameof(Player), id);
            }

            return match;
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
    }
}
