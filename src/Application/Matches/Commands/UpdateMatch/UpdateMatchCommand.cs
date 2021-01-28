using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Matches.Commands.UpdateMatch
{
    public class UpdateMatchCommand : IRequest
    {
        public int Id { get; set; }
        public string KickOff { get; set; }
        public int GameWeek { get; set; }
    }

    public class UpdateMatchCommandHandler : IRequestHandler<UpdateMatchCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateMatchCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateMatchCommand request, CancellationToken cancellationToken)
        {
            var match = await GetMatch(request.Id);

            match.KickOff = DateTime.Parse(request.KickOff);
            match.GameWeek = request.GameWeek;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
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
