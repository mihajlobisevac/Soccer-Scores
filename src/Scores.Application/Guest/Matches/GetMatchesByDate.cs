using Scores.Application.Guest.Clubs;
using Scores.Application.StandingsAdmin;
using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;

namespace Scores.Application.Guest.Matches
{
    [Service]
    public class GetMatchesByDate
    {
        private readonly IMatchManager matchManager;
        private readonly IClubManager clubManager;
        private readonly IStandingsManager standingsManager;

        public GetMatchesByDate(IMatchManager matchManager, IClubManager clubManager, IStandingsManager standingsManager)
        {
            this.matchManager = matchManager;
            this.clubManager = clubManager;
            this.standingsManager = standingsManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public DateTime KickOff { get; set; }
            public GetClubById.Response HomeTeam { get; set; }
            public GetClubById.Response AwayTeam { get; set; }
            public GetStandings.Response Standings { get; set; }
        }

        public IEnumerable<Response> Do()
        {
            var getClub = new GetClubById(clubManager);
            var getStandings = new GetStandings(standingsManager);

            return matchManager.GetMatchesByDate(DateTime.Now, (match)
                => new Response
                {
                    Id = match.Id,
                    KickOff = match.KickOff,
                    HomeTeam = getClub.Do(match.HomeTeamId),
                    AwayTeam = getClub.Do(match.AwayTeamId),
                    Standings = getStandings.Do(match.StandingsId),
                });
        }
    }
}
