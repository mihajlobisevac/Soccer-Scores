using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.Guest.Players
{
    [Service]
    public class GetPlayerById
    {
        private readonly IPlayerManager playerManager;

        public GetPlayerById(IPlayerManager playerManager)
        {
            this.playerManager = playerManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Position { get; set; }
            public int ShirtNumber { get; set; }
        }

        public Response Do(int id)
        {
            return playerManager.GetPlayerById(id, (player)
                => new Response
                {
                    Id = player.Id,
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    Position = player.Position,
                    ShirtNumber = player.ShirtNumber,
                });
        }
    }
}
