using Scores.Domain.Infrastructure;
using Scores.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Scores.Application.PlayersAdmin
{
    [Service]
    public class CreatePlayer
    {
        private readonly IPlayerManager playerManager;

        public CreatePlayer(IPlayerManager playerManager)
        {
            this.playerManager = playerManager;
        }

        public class Request
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DoB { get; set; }
            public int Height { get; set; }
            public int Weight { get; set; }
            public string Position { get; set; }
            public int ShirtNumber { get; set; }
            public string Foot { get; set; }
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
            public int ClubId { get; set; }
            public int NationalityId { get; set; }
            public int PoBId { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var player = new Player
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DoB = request.DoB,
                Height = request.Height,
                Weight = request.Weight,
                Position = request.Position,
                ShirtNumber = request.ShirtNumber,
                Foot = request.Foot,
                ClubId = request.ClubId,
                NationalityCountryId = request.NationalityId,
                PoBCityId = request.PoBId,
            };

            var result = await playerManager.CreatePlayer(player);

            if (result <= 0)
                throw new Exception("Failed to create player");

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
