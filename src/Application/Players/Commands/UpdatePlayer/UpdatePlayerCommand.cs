using Application.Common.Extensions;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Players.Commands.UpdatePlayer
{
    public class UpdatePlayerCommand : IRequest
    {
        public int Id { get; set; }
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

    public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand>
    {
        private readonly IApplicationDbContext context;

        public UpdatePlayerCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = await GetPlayer(request.Id);
            var nationality = await GetCountry(request.NationalityId);
            var placeOfBirth = await GetCity(request.PlaceOfBirthId);

            player.FirstName = request.FirstName;
            player.LastName = request.LastName;
            player.DateOfBirth = DateTime.Parse(request.DateOfBirth);
            player.Position = request.Position.ToEnum<Position>();
            player.Foot = request.Foot.ToEnum<Foot>();
            player.Height = request.Height;
            player.Weight = request.Weight;
            player.Nationality = nationality;
            player.PlaceOfBirth = placeOfBirth;

            await UpdateClubPlayer(request, player);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<Player> GetPlayer(int id)
        {
            var player = await context.Players.FindAsync(id);

            if (player is null)
            {
                throw new NotFoundException(nameof(Player), id);
            }

            return player;
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

        private async Task<Club> GetClub(int id) => await context.Clubs.FindAsync(id);
        private async Task<ClubPlayer> GetClubPlayer(int playerId) => await context.ClubPlayers.FirstOrDefaultAsync(x => x.Player.Id == playerId);

        private async Task UpdateClubPlayer(UpdatePlayerCommand request, Player player)
        {
            var club = await GetClub(request.ClubId);

            if (club is not null)
            {
                var clubPlayer = await GetClubPlayer(request.Id);

                if (clubPlayer is not null)
                {
                    clubPlayer.Club = club;
                    clubPlayer.ShirtNumber = request.ShirtNumber;
                }
                else
                {
                    AddPlayerToClub(player, club, request);
                }
            }
        }

        private void AddPlayerToClub(Player player, Club club, UpdatePlayerCommand request)
        {
            context.ClubPlayers.Add(new ClubPlayer
                {
                    ShirtNumber = request.ShirtNumber,
                    Player = player,
                    Club = club,
                });
        }
    }
}
