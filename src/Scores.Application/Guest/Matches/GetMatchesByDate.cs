using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Events;
using Scores.Application.MatchesAdmin;
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
        private readonly IEventManager eventManager;

        public GetMatchesByDate(IMatchManager matchManager, IClubManager clubManager, 
            IStandingsManager standingsManager, IEventManager eventManager)
        {
            this.matchManager = matchManager;
            this.clubManager = clubManager;
            this.standingsManager = standingsManager;
            this.eventManager = eventManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public DateTime KickOff { get; set; }
            public GetClubById.Response HomeTeam { get; set; }
            public GetClubById.Response AwayTeam { get; set; }
            public GetStandings.Response Standings { get; set; }
            public IEnumerable<GetEventsByMatchId.Response> MatchInfo { get; set; }
        }

        public IEnumerable<Response> Do(DateTime date)
        {
            var getClub = new GetClubById(clubManager);
            var getStandings = new GetStandings(standingsManager);
            var getEvents = new GetEventsByMatchId(eventManager);

            return matchManager.GetMatchesByDate(date, (match)
                => new Response
                {
                    Id = match.Id,
                    KickOff = match.KickOff,
                    HomeTeam = getClub.Do(match.HomeTeamId),
                    AwayTeam = getClub.Do(match.AwayTeamId),
                    Standings = getStandings.Do(match.StandingsId),
                    MatchInfo = getEvents.Do(match.Id),
                });
        }
    }
}
