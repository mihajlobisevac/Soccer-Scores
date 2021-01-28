using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Flag { get; set; }
    }

    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateCountryCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var entity = await GetCountry(request.Id);

            entity.Name = request.Name;
            entity.Flag = request.Flag;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<Country> GetCountry(int id)
        {
            var country = await context.Countries.FindAsync(id);

            if (country is null)
            {
                throw new NotFoundException(nameof(Country), id);
            }

            return country;
        }
    }
}
