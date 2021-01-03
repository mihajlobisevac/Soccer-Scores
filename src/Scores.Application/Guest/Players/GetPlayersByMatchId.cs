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
        private readonly IClubManager clubManager;
        private readonly IVenueManager venueManager;
        private readonly ICityManager cityManager;
        private readonly ICountryManager countryManager;

        public GetPlayersByMatchId(IPlayerManager playerManager, IClubManager clubManager,
            IVenueManager venueManager, ICityManager cityManager, ICountryManager countryManager)
        {
            this.playerManager = playerManager;
            this.clubManager = clubManager;
            this.venueManager = venueManager;
            this.cityManager = cityManager;
            this.countryManager = countryManager;
        }

        public class Response
        {
            public bool IsHome { get; set; }
            public bool IsSubstitute { get; set; }

            public GetPlayerById.Response Player { get; set; }
        }

        public IEnumerable<Response> Do(int id)
        {
            var getPlayer = new GetPlayerById(playerManager, clubManager, venueManager, cityManager, countryManager);

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
