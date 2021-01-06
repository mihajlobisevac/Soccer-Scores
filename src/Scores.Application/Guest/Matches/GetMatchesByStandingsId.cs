using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Events;
using Scores.Application.StandingsAdmin;
using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.Guest.Matches
{
    [Service]
    public class GetMatchesByStandingsId
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

        public GetMatchesByStandingsId(IMatchManager matchManager, IClubManager clubManager, IVenueManager venueManager,
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

        public IEnumerable<GetMatchById.Response> Do(int id)
        {
            var getMatch = new GetMatchById(matchManager, clubManager, venueManager, eventManager,
                cityManager, countryManager, standingsManager, tournamentManager, playerManager);

            return matchManager
                .GetMatchesByStandingsId(id, (match) => getMatch.Do(match.Id));
        }
    }
}
