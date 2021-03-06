﻿using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Seasons.Commands.DeleteSeason
{
    public class DeleteSeasonCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteSeasonCommandHandler : IRequestHandler<DeleteSeasonCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteSeasonCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteSeasonCommand request, CancellationToken cancellationToken)
        {
            var season = await GetSeason(request.Id);

            context.Seasons.Remove(season);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
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
