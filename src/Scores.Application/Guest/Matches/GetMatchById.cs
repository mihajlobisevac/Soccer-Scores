using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Standings;
using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.Guest.Matches
{
    [Service]
    public class GetMatchById
    {
        private readonly IMatchManager matchManager;
        private readonly IClubManager clubManager;
        private readonly IVenueManager venueManager;
        private readonly ICityManager cityManager;
        private readonly ICountryManager countryManager;
        private readonly IStandingsManager standingsManager;
        private readonly ITournamentManager tournamentManager;

        public GetMatchById(IMatchManager matchManager, IClubManager clubManager, IVenueManager venueManager,
            ICityManager cityManager, ICountryManager countryManager, IStandingsManager standingsManager, ITournamentManager tournamentManager)
        {
            this.matchManager = matchManager;
            this.clubManager = clubManager;
            this.venueManager = venueManager;
            this.cityManager = cityManager;
            this.countryManager = countryManager;
            this.standingsManager = standingsManager;
            this.tournamentManager = tournamentManager;
        }

        public class Response
        {
            public DateTime KickOff { get; set; }
            public GetClubById.Response HomeTeam { get; set; }
            public GetClubById.Response AwayTeam { get; set; }
            public GetStandingsById.Response Standings { get; set; }
        }

        public Response Do(int id)
        {
            var getClub = new GetClubById(clubManager, venueManager, cityManager, countryManager);
            var getStandings = new GetStandingsById(standingsManager, tournamentManager, countryManager);

            return matchManager.GetMatchById(id, x => new Response
            {
                KickOff = x.KickOff,
                HomeTeam = getClub.Do(x.HomeTeamId),
                AwayTeam = getClub.Do(x.AwayTeamId),
                Standings = getStandings.Do(x.StandingsId),
            });
        }
    }
}
