using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Competitions.Commands.UpdateCompetition
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
            var city = await context.Competitions.FindAsync(request.Id);

            if (city == null)
            {
                throw new NotFoundException(nameof(Competition), request.Id);
            }

            city.Name = request.Name;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
