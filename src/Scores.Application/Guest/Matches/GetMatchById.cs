using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Events;
using Scores.Application.Guest.Standings;
using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;

namespace Scores.Application.Guest.Matches
{
    [Service]
    public class GetMatchById
    {
        private readonly IMatchManager matchManager;

        public GetMatchById(IMatchManager matchManager)
        {
            this.matchManager = matchManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public DateTime KickOff { get; set; }
            public GetClubById.Response HomeTeam { get; set; }
            public GetClubById.Response AwayTeam { get; set; }
            public GetStandingsById.Response Standings { get; set; }
            public IEnumerable<GetEventsByMatchId.Response> Incidents { get; set; }
        }

        public Response Do(
            int id, 
            GetClubById getClub, 
            GetStandingsById getStandings, 
            GetEventsByMatchId getEvents)
        {
            return matchManager
                .GetMatchById(id, x => new Response
                    {
                        Id = x.Id,
                        KickOff = x.KickOff,
                        HomeTeam = getClub.Do(x.HomeTeamId),
                        AwayTeam = getClub.Do(x.AwayTeamId),
                        Standings = getStandings.Do(x.StandingsId),
                        Incidents = getEvents.Do(x.Id),
                    });
        }
    }
}
