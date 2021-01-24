using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Cities.Commands.DeleteCity
{
    public class DeleteCityCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteCityCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Cities.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(City), request.Id);
            }

            context.Cities.Remove(entity);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
