using Scores.Application.Guest.Players;
using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.Guest.Events
{
    [Service]
    public class GetEventsByMatchId
    {
        private readonly IEventManager eventManager;
        private readonly IPlayerManager playerManager;
        private readonly IClubManager clubManager;
        private readonly IVenueManager venueManager;
        private readonly ICityManager cityManager;
        private readonly ICountryManager countryManager;

        public GetEventsByMatchId(IEventManager eventManager, IPlayerManager playerManager, IClubManager clubManager,
            IVenueManager venueManager, ICityManager cityManager, ICountryManager countryManager)
        {
            this.eventManager = eventManager;
            this.playerManager = playerManager;
            this.clubManager = clubManager;
            this.venueManager = venueManager;
            this.cityManager = cityManager;
            this.countryManager = countryManager;
        }

        public class Response
        {
            public int HomeScore { get; set; }
            public int AwayScore { get; set; }
            public int Minute { get; set; }
            public bool IsHome { get; set; }
            public string Type { get; set; }
            public string Class { get; set; }
            public GetPlayerById.Response PlayerA { get; set; }
            public GetPlayerById.Response PlayerB { get; set; }
        }

        public IEnumerable<Response> Do(int matchId)
        {
            var getPlayer = new GetPlayerById(playerManager, clubManager, venueManager, cityManager, countryManager);

            return eventManager.GetEventsByMatchId(matchId, x => new Response
            {
                HomeScore = x.HomeScore,
                AwayScore = x.AwayScore,
                Minute = x.Minute,
                IsHome = x.IsHome,
                Type = x.Type,
                Class = x.Class,
                PlayerA = getPlayer.Do(x.PlayerAId),
                PlayerB = getPlayer.Do(x.PlayerBId),
            });
        }
    }
}
