using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Events;
using Scores.Application.Guest.Players;
using Scores.Application.Guest.Standings;
using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;

namespace Scores.Application.Guest.Matches
{
    [Service]
    public class GetMatchesByDate
    {
        private readonly IMatchManager matchManager;

        public GetMatchesByDate(IMatchManager matchManager)
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

        public IEnumerable<Response> Do(
            DateTime date, 
            GetClubById getClub, 
            GetStandingsById getStandings, 
            GetEventsByMatchId getEvents,
            GetPlayerById getPlayer)
        {
            return matchManager.GetMatchesByDate(date, (match)
                => new Response
                {
                    Id = match.Id,
                    KickOff = match.KickOff,
                    HomeTeam = getClub.Do(match.HomeTeamId),
                    AwayTeam = getClub.Do(match.AwayTeamId),
                    Standings = getStandings.Do(match.StandingsId),
                    Incidents = getEvents.Do(match.Id, getPlayer),
                });
        }
    }
}
