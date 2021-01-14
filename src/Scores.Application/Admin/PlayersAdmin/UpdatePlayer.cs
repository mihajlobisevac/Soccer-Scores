using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scores.Application.PlayersAdmin
{
    [Service]
    public class UpdatePlayer
    {
        private readonly IPlayerManager playerManager;

        public UpdatePlayer(IPlayerManager playerManager)
        {
            this.playerManager = playerManager;
        }

        public class Request
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DoB { get; set; }
            public int Height { get; set; }
            public int Weight { get; set; }
            public string Position { get; set; }
            public int ShirtNumber { get; set; }
            public string Foot { get; set; }
            public bool Deactivated { get; set; }
            public int ClubId { get; set; }
            public int NationalityId { get; set; }
            public int PoBId { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DoB { get; set; }
            public int Height { get; set; }
            public int Weight { get; set; }
            public string Position { get; set; }
            public int ShirtNumber { get; set; }
            public string Foot { get; set; }
            public bool Deactivated { get; set; }
            public int ClubId { get; set; }
            public int NationalityId { get; set; }
            public int PoBId { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var player = playerManager.GetPlayerById(request.Id, x => x);

            player.FirstName = request.FirstName;
            player.LastName = request.LastName;
            player.DoB = request.DoB;
            player.Height = request.Height;
            player.Weight = request.Weight;
            player.Position = request.Position;
            player.ShirtNumber = request.ShirtNumber;
            player.Foot = request.Foot;
            player.ClubId = request.ClubId;
            player.NationalityCountryId = request.NationalityId;
            player.PoBCityId = request.PoBId;

            await playerManager.UpdatePlayer(player);

            return new Response
            {
                Id = player.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DoB = request.DoB,
                Height = request.Height,
                Weight = request.Weight,
                Position = request.Position,
                ShirtNumber = request.ShirtNumber,
                Foot = request.Foot,
                ClubId = request.ClubId,
                NationalityId = request.NationalityId,
                PoBId = request.PoBId,
            };
        }
    }
}
