using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Events;
using Scores.Application.Guest.Tournaments;
using Scores.Application.StandingsAdmin;
using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scores.Application.Guest.Matches
{
    [Service]
    public class GetFixtures
    {
        private readonly IMatchManager matchManager;
        private readonly IClubManager clubManager;
        private readonly IEventManager eventManager;
        private readonly IStandingsManager standingsManager;
        private readonly ITournamentManager tournamentManager;
        private readonly ICountryManager countryManager;
        private readonly IVenueManager venueManager;
        private readonly ICityManager cityManager;
        private readonly IPlayerManager playerManager;

        public GetFixtures(IMatchManager matchManager, IClubManager clubManager, IEventManager eventManager,
            IStandingsManager standingsManager, ITournamentManager tournamentManager, ICountryManager countryManager,
            IVenueManager venueManager, ICityManager cityManager, IPlayerManager playerManager)
        {
            this.matchManager = matchManager;
            this.clubManager = clubManager;
            this.eventManager = eventManager;
            this.standingsManager = standingsManager;
            this.tournamentManager = tournamentManager;
            this.countryManager = countryManager;
            this.venueManager = venueManager;
            this.cityManager = cityManager;
            this.playerManager = playerManager;
        }

        public class Response
        {
            public GetTournamentById.Response Tournament { get; set; }
            public List<MatchViewModel> Matches { get; set; } = new List<MatchViewModel>();
        }

        public class MatchViewModel
        {
            public int Id { get; set; }
            public DateTime KickOff { get; set; }
            public GetClubById.Response HomeTeam { get; set; }
            public GetClubById.Response AwayTeam { get; set; }
            public GetStandings.Response Standings { get; set; }
            public IEnumerable<GetEventsByMatchId.Response> MatchInfo { get; set; }
        }

        public IEnumerable<Response> DoByDate(DateTime date)
        {
            var matches = new GetMatchesByDate(matchManager, clubManager, standingsManager, 
                eventManager, venueManager, cityManager, countryManager, playerManager)
                    .Do(date)
                    .GroupBy(item => item.Standings.TournamentId)
                    .Select(group => group.ToList())
                    .ToList();

            List<Response> Fixtures = new List<Response>();

            int tournamentId = -1;

            for (int i = 0; i < matches.Count; i++)
            {
                foreach (var match in matches[i])
                {
                    if (tournamentId != match.Standings.TournamentId)
                    {
                        tournamentId = match.Standings.TournamentId;

                        Fixtures.Add(new Response
                        {
                            Tournament = new GetTournamentById(tournamentManager, countryManager)
                                .Do(match.Standings.TournamentId)
                        });
                    }

                    Fixtures[i].Matches.Add(new MatchViewModel 
                    {
                        Id = match.Id,
                        KickOff = match.KickOff,
                        HomeTeam = match.HomeTeam,
                        AwayTeam = match.AwayTeam,
                        Standings = match.Standings,
                        MatchInfo = match.MatchInfo,
                    });
                }
            }

            return Fixtures;
        }
    }
}
