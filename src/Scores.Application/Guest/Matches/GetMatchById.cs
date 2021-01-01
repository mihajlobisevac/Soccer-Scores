using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Events;
using Scores.Application.Guest.Standings;
using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scores.Application.Guest.Matches
{
    [Service]
    public class GetMatchById
    {
        private readonly IMatchManager matchManager;
        private readonly IClubManager clubManager;
        private readonly IVenueManager venueManager;
        private readonly IEventManager eventManager;
        private readonly ICityManager cityManager;
        private readonly ICountryManager countryManager;
        private readonly IStandingsManager standingsManager;
        private readonly ITournamentManager tournamentManager;
        private readonly IPlayerManager playerManager;

        public GetMatchById(IMatchManager matchManager, IClubManager clubManager, IVenueManager venueManager, 
            IEventManager eventManager, ICityManager cityManager, ICountryManager countryManager, IStandingsManager standingsManager, 
            ITournamentManager tournamentManager, IPlayerManager playerManager)
        {
            this.matchManager = matchManager;
            this.clubManager = clubManager;
            this.venueManager = venueManager;
            this.eventManager = eventManager;
            this.cityManager = cityManager;
            this.countryManager = countryManager;
            this.standingsManager = standingsManager;
            this.tournamentManager = tournamentManager;
            this.playerManager = playerManager;
        }

        public class Response
        {
            public DateTime KickOff { get; set; }
            public GetClubById.Response HomeTeam { get; set; }
            public GetClubById.Response AwayTeam { get; set; }
            public GetStandingsById.Response Standings { get; set; }
            public List<GetEventsByMatchId.Response> Events { get; set; }
        }

        public Response Do(int id)
        {
            var getClub = new GetClubById(clubManager, venueManager, cityManager, countryManager);
            var getStandings = new GetStandingsById(standingsManager, tournamentManager, countryManager);
            var getEvents = new GetEventsByMatchId(eventManager, playerManager);

            return matchManager.GetMatchById(id, x => new Response
            {
                KickOff = x.KickOff,
                HomeTeam = getClub.Do(x.HomeTeamId),
                AwayTeam = getClub.Do(x.AwayTeamId),
                Standings = getStandings.Do(x.StandingsId),
                Events = getEvents.Do(x.Id).ToList(),
            });
        }
    }
}
