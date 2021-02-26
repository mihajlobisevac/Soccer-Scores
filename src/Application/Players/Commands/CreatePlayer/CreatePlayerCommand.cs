using Application.Common.Extensions;
using Domain.Enums;
using MediatR;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Players.Commands.CreatePlayer
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
            var player = new Player
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = DateTime.Parse(request.DateOfBirth),
                Position = request.Position.ToEnum<Position>(),
                Foot = request.Foot.ToEnum<Foot>(),
                Height = request.Height,
                Weight = request.Weight,
                Nationality = await GetCountry(request.NationalityId),
                PlaceOfBirth = await GetCity(request.PlaceOfBirthId),
            };

            context.Players.Add(player);

            await HandleClubPlayer(player, request);

            await context.SaveChangesAsync(cancellationToken);

            return player.Id;
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

        private async Task<City> GetCity(int id)
        {
            var city = await context.Cities.FindAsync(id);

            if (city is null)
            {
                throw new NotFoundException(nameof(City), id);
            }

            return city;
        }

        private async Task HandleClubPlayer(Player entity, CreatePlayerCommand request)
        {
            var club = await context.Clubs.FindAsync(request.ClubId);

            if (club is not null)
            {
                context.ClubPlayers.Add(new ClubPlayer
                {
                    ShirtNumber = request.ShirtNumber,
                    Player = entity,
                    Club = club,
                });
            }
        }
    }
}
