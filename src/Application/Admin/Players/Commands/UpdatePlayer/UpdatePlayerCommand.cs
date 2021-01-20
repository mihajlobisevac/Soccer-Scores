using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoccerScores.Application.Common.Exceptions;
using SoccerScores.Application.Common.Interfaces;
using SoccerScores.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerScores.Application.Admin.Players.Commands.UpdatePlayer
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
            var player = await context.Players.FindAsync(request.Id) 
                ?? throw new NotFoundException(nameof(Player), request.Id);

            var nationality = await context.Countries.FindAsync(request.NationalityId)
                ?? throw new NotFoundException(nameof(Country), request.NationalityId);

            var placeOfBirth = await context.Cities.FindAsync(request.PlaceOfBirthId)
                ?? throw new NotFoundException(nameof(City), request.PlaceOfBirthId);

            player.FirstName = request.FirstName;
            player.LastName = request.LastName;
            player.DateOfBirth = DateTime.Parse(request.DateOfBirth);
            player.Position = (Position)Enum.Parse(typeof(Position), request.Position, false);
            player.Foot = (Foot)Enum.Parse(typeof(Foot), request.Foot, false);
            player.Height = request.Height;
            player.Weight = request.Weight;
            player.Nationality = nationality;
            player.PlaceOfBirth = placeOfBirth;

            var club = await context.Clubs.FindAsync(request.ClubId);
            var clubPlayer = await context.ClubPlayers.FirstOrDefaultAsync(x => x.Player.Id == request.Id);

            if (club != null)
            {
                if (clubPlayer == null)
                {
                    context.ClubPlayers.Add(new ClubPlayer
                    {
                        ShirtNumber = request.ShirtNumber,
                        Player = player,
                        Club = club,
                    });
                }
                else
                {
                    clubPlayer.Club = club;
                    clubPlayer.ShirtNumber = request.ShirtNumber;
                }
            }

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
