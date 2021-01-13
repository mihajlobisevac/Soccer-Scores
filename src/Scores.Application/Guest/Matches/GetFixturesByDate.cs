using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Events;
using Scores.Application.Guest.Standings;
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
    public class GetFixturesByDate
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

        public GetFixturesByDate(IMatchManager matchManager, IClubManager clubManager, IEventManager eventManager,
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
            public GetStandingsById.Response Standings { get; set; }
            public List<MatchViewModel> Matches { get; set; }

            public Response(GetStandingsById.Response standings)
            {
                Standings = standings;
                Matches = new List<MatchViewModel>();
            }
        }

        public class MatchViewModel
        {
            public int Id { get; set; }
            public DateTime KickOff { get; set; }
            public GetClubById.Response HomeTeam { get; set; }
            public GetClubById.Response AwayTeam { get; set; }
            public GetStandings.Response Standings { get; set; }
            public IEnumerable<GetEventsByMatchId.Response> Incidents { get; set; }
        }

        public IEnumerable<Response> Do(DateTime date)
        {
            var tournaments = GetTournamentsWithMatches(date);

            return CreateFixtures(tournaments);
        }

        private List<List<MatchViewModel>> GetTournamentsWithMatches(DateTime date)
        {
            var matches = GetMatchesByDate(date);
            var tournaments = ExtractTournamentsFromMatches(matches);

            return tournaments;
        }

        private IEnumerable<MatchViewModel> GetMatchesByDate(DateTime date)
        {
            var getMatches = new GetMatchesByDate(matchManager, clubManager, standingsManager,
                eventManager, venueManager, cityManager, countryManager, playerManager);

            return getMatches.Do(date)
                .Select(x => new MatchViewModel
                {
                    Id = x.Id,
                    KickOff = x.KickOff,
                    HomeTeam = x.HomeTeam,
                    AwayTeam = x.AwayTeam,
                    Standings = x.Standings,
                    Incidents = x.Incidents,
                });
        }

        private List<List<MatchViewModel>> ExtractTournamentsFromMatches(IEnumerable<MatchViewModel> matches)
        {
            var extractedTournaments = matches
                .GroupBy(item => item.Standings.Id)
                .Select(group => group.ToList())
                .ToList();

            return extractedTournaments;
        }

        private List<Response> CreateFixtures(List<List<MatchViewModel>> tournaments)
        {
            List<Response> Fixtures = new List<Response>();

            var getStandings = new GetStandingsById(standingsManager, tournamentManager, countryManager);

            int tournamentId = -1;

            for (int i = 0; i < tournaments.Count; i++)
            {
                foreach (var match in tournaments[i])
                {
                    if (tournamentId != match.Standings.TournamentId)
                    {
                        tournamentId = match.Standings.TournamentId;

                        Fixtures.Add(new Response(getStandings.Do(match.Standings.Id)));
                    }

                    Fixtures[i].Matches.Add(match);
                }
            }

            return Fixtures;
        }
    }
}
