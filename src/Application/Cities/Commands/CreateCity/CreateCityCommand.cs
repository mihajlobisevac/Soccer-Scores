using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Cities.Commands.CreateCity
{
    public class CreateCityCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
    }

    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateCityCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var entity = new City
            {
                Name = request.Name,
                Country = await context.Countries.FindAsync(request.CountryId) ??
                    throw new NotFoundException(nameof(Country), request.CountryId)
            };

            context.Cities.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
