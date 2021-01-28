using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Clubs.Commands.CreateClub
{
    public class CreateClubCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Crest { get; set; }
        public string Venue { get; set; }
        public int YearFounded { get; set; }
        public int CityId { get; set; }
    }

    public class CreateClubCommandHandler : IRequestHandler<CreateClubCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreateClubCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreateClubCommand request, CancellationToken cancellationToken)
        {
            var entity = new Club
            {
                Name = request.Name,
                Crest = request.Crest,
                Venue = request.Venue,
                YearFounded = request.YearFounded,
                City = await GetCity(request.CityId),
            };

            context.Clubs.Add(entity);

            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
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
