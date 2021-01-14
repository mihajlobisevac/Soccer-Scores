using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Events;
using Scores.Application.Guest.Players;
using Scores.Application.Guest.Standings;
using Scores.Domain.Infrastructure;
using System.Collections.Generic;

namespace Scores.Application.Guest.Matches
{
    [Service]
    public class GetMatchesByPlayerId
    {
        private readonly IMatchManager matchManager;

        public GetMatchesByPlayerId(IMatchManager matchManager)
        {
            this.matchManager = matchManager;
        }

        public IEnumerable<GetMatchById.Response> Do(
            int id, 
            GetMatchById getMatch,
            GetClubById getClub,
            GetStandingsById getStandings,
            GetEventsByMatchId getEvents,
            GetPlayerById getPlayer)
        {
            return matchManager
                .GetMatchesByPlayerId(id, (match) => getMatch
                    .Do(match.MatchId, getClub, getStandings, getEvents, getPlayer));
        }
    }
}
