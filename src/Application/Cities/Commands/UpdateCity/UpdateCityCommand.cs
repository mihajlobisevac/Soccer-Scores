using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Cities.Commands.UpdateCity
{
    public class UpdateCityCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateCityCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var city = await GetCity(request.Id);

            city.Name = request.Name;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<City> GetCity(int id)
        {
            var city = await context.Cities.FindAsync(id);

            if (city is null)
            {
                throw new NotFoundException(nameof(City), id);
            }

            return city;
        }
    }
}
