using Scores.Application.Guest.Clubs;
using Scores.Application.Guest.Events;
using Scores.Application.Guest.Standings;
using Scores.Domain.Infrastructure;
using System.Collections.Generic;

namespace Scores.Application.Guest.Matches
{
    [Service]
    public class GetMatchesByStandingsId
    {
        private readonly IMatchManager matchManager;

        public GetMatchesByStandingsId(IMatchManager matchManager)
        {
            this.matchManager = matchManager;
        }

        public IEnumerable<GetMatchById.Response> Do(
            int id, 
            GetMatchById getMatch,
            GetClubById getClub,
            GetStandingsById getStandings,
            GetEventsByMatchId getEvents)
        {
            return matchManager
                .GetMatchesByStandingsId(id, (match) => getMatch
                    .Do(match.Id, getClub, getStandings, getEvents));
        }
    }
}
