using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Seasons.Commands.CreateSeason
{
    public class CreateSeasonCommand : IRequest<int>
    {
        public string Start { get; set; }
        public string End { get; set; }
        public int CompetitionId { get; set; }
    }

    public class CreateSeasonCommandHandler : IRequestHandler<CreateSeasonCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateSeasonCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
        {
            var season = new Season
            {
                Start = DateTime.Parse(request.Start),
                End = DateTime.Parse(request.End),
                Competition = await GetCompetition(request.CompetitionId)
            };

            context.Seasons.Add(season);

            await context.SaveChangesAsync(cancellationToken);

            return season.Id;
        }

        private async Task<Competition> GetCompetition(int id)
        {
            var competition = await context.Competitions.FindAsync(id);

            if (competition is null)
            {
                throw new NotFoundException(nameof(Competition), id);
            }

            return competition;
        }
    }
}
