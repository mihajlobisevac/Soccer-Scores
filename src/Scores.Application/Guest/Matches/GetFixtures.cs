using Scores.Application.Guest.Tournaments;
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

        public GetFixtures(IMatchManager matchManager, IClubManager clubManager, IEventManager eventManager,
            IStandingsManager standingsManager, ITournamentManager tournamentManager, ICountryManager countryManager)
        {
            this.matchManager = matchManager;
            this.clubManager = clubManager;
            this.eventManager = eventManager;
            this.standingsManager = standingsManager;
            this.tournamentManager = tournamentManager;
            this.countryManager = countryManager;
        }

        public class Response
        {
            public GetTournamentById.Response Tournament { get; set; }
            public List<GetMatchesByDate.Response> Matches { get; set; } = new List<GetMatchesByDate.Response>();
        }

        public IEnumerable<Response> Do(DateTime date)
        {
            var matches = new GetMatchesByDate(matchManager, clubManager, standingsManager, eventManager)
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

                    Fixtures[i].Matches.Add(match);
                }
            }

            return Fixtures;
        }
    }
}
