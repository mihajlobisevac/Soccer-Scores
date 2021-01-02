using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.Guest.Players
{
    [Service]
    public class GetPlayersByMatchId
    {
        private readonly IPlayerManager playerManager;

        public GetPlayersByMatchId(IPlayerManager playerManager)
        {
            this.playerManager = playerManager;
        }

        public class Response
        {
            public bool IsHome { get; set; }
            public bool IsSubstitute { get; set; }

            public GetPlayerById.Response Player { get; set; }
        }

        public IEnumerable<Response> Do(int id)
        {
            var getPlayer = new GetPlayerById(playerManager);

            return playerManager.GetPlayersByMatchId(id, (matchPlayer)
                => new Response
                {
                    IsHome = matchPlayer.IsHome,
                    IsSubstitute = matchPlayer.IsSubstitute,
                    Player = getPlayer.Do(matchPlayer.PlayerId),
                });
        }
    }
}
