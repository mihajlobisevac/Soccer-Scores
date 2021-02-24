using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Countries.Commands.DeleteCountry
{
    public class DeleteCountryCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteCountryCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await GetCountry(request.Id);

            context.Countries.Remove(country);

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
