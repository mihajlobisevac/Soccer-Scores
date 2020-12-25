using Scores.Domain.Infrastructure;
using System;

namespace Scores.Application.PlayersAdmin
{
    [Service]
    public class GetPlayer
    {
        private readonly IPlayerManager playerManager;

        public GetPlayer(IPlayerManager playerManager)
        {
            this.playerManager = playerManager;
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

        public Response Do(int id) =>
            playerManager.GetPlayerById(id, x => new Response
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DoB = x.DoB,
                Height = x.Height,
                Weight = x.Weight,
                Position = x.Position,
                ShirtNumber = x.ShirtNumber,
                Foot = x.Foot,
                Deactivated = x.Deactivated,
                ClubId = x.ClubId,
                NationalityId = x.NationalityCountryId,
                PoBId = x.PoBCityId,
            });
    }
}
