using Domain.Enums;
using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerStreams.Application.Admin.Competitions.Commands.CreateCompetition
{
    public class CreateCompetitionCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public int CountryId { get; set; }
    }

    public class CreateCompetitionCommandHandler : IRequestHandler<CreateCompetitionCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateCompetitionCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateCompetitionCommand request, CancellationToken cancellationToken)
        {
            var entity = new Competition
            {
                Name = request.Name,
                Type = (CompetitionType)request.Type,
                Country = await context.Countries.FindAsync(request.CountryId) ??
                    throw new NotFoundException(nameof(Country), request.CountryId)
            };

            context.Competitions.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
