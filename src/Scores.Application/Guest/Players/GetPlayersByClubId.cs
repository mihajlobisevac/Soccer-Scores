using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.Guest.Players
{
    [Service]
    public class GetPlayersByClubId
    {
        private readonly IPlayerManager playerManager;

        public GetPlayersByClubId(IPlayerManager playerManager)
        {
            this.playerManager = playerManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DoB { get; set; }
            public string Position { get; set; }
            public int ShirtNumber { get; set; }
        }

        public IEnumerable<Response> Do(int id)
        {
            return playerManager.GetPlayersByClubId(id, (player)
                => new Response
                {
                    Id = player.Id,
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    DoB = player.DoB,
                    Position = player.Position,
                    ShirtNumber = player.ShirtNumber,
                });
        }
    }
}
