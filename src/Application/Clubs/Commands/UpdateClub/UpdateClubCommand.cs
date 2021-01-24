using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Clubs.Commands.UpdateClub
{
    public class UpdateClubCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Crest { get; set; }
        public string Venue { get; set; }
        public int YearFounded { get; set; }
        public int CityId { get; set; }
    }

    public class UpdateClubCommandHandler : IRequestHandler<UpdateClubCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdateClubCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateClubCommand request, CancellationToken cancellationToken)
        {
            var club = await context.Clubs.FindAsync(request.Id)
                ?? throw new NotFoundException(nameof(Club), request.Id);

            var city = await context.Cities.FindAsync(request.CityId)
                ?? throw new NotFoundException(nameof(City), request.CityId);

            club.Name = request.Name;
            club.Crest = request.Crest;
            club.Venue = request.Venue;
            club.YearFounded = request.YearFounded;
            club.City = city;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
