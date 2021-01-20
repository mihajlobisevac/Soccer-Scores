using Domain.Enums;
using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Position { get; set; }
        public string Foot { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        public int NationalityId { get; set; }
        public int PlaceOfBirthId { get; set; }

        public int ClubId { get; set; }
        public int ShirtNumber { get; set; }
    }

    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, int>
    {
        private readonly IApplicationDbContext context;

        public CreatePlayerCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var nationality = await context.Countries.FindAsync(request.NationalityId) 
                ?? throw new NotFoundException(nameof(Country), request.NationalityId);

            var placeOfBirth = await context.Cities.FindAsync(request.PlaceOfBirthId)
                ?? throw new NotFoundException(nameof(City), request.PlaceOfBirthId);

            var club = await context.Clubs.FindAsync(request.ClubId);

            var entity = new Player
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = DateTime.Parse(request.DateOfBirth),
                Position = (Position) Enum.Parse(typeof(Position), request.Position, false),
                Foot = (Foot) Enum.Parse(typeof(Foot), request.Foot, false),
                Height = request.Height,
                Weight = request.Weight,
                Nationality = nationality,
                PlaceOfBirth = placeOfBirth,
            };

            context.Players.Add(entity);

            if (club != null)
            {
                var clubPlayer = new ClubPlayer
                {
                    ShirtNumber = request.ShirtNumber,
                    Player = entity,
                    Club = club,
                };

                context.ClubPlayers.Add(clubPlayer);
            }

            await context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
