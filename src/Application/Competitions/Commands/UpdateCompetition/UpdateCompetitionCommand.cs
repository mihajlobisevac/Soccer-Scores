using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Competitions.Commands.UpdateCompetition
{
    public class UpdateCompetitionCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateCompetitionCommandHandler : IRequestHandler<UpdateCompetitionCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateCompetitionCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateCompetitionCommand request, CancellationToken cancellationToken)
        {
            var competition = await GetCompetition(request.Id);

            competition.Name = request.Name;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
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
