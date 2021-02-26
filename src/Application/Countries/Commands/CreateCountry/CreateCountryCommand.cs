using MediatR;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Countries.Commands.CreateCountry
{
    public class CreateCountryCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Flag { get; set; }
    }

    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateCountryCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = new Country
            {
                Name = request.Name,
                Flag = request.Flag
            };

            context.Countries.Add(country);

            await context.SaveChangesAsync(cancellationToken);

            return country.Id;
        }
    }
}
