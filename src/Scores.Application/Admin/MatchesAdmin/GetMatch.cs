using Scores.Domain.Infrastructure;
using System;

namespace Scores.Application.MatchesAdmin
{
    [Service]
    public class GetMatch
    {
        private readonly IMatchManager matchManager;

        public GetMatch(IMatchManager matchManager)
        {
            this.matchManager = matchManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public DateTime KickOff { get; set; }
            public int HomeTeamId { get; set; }
            public int AwayTeamId { get; set; }
        }

        public Response Do(int id) =>
            matchManager.GetMatchById(id, x => new Response
            {
                Id = x.Id,
                KickOff = x.KickOff,
                HomeTeamId = x.HomeTeamId,
                AwayTeamId = x.AwayTeamId,
            });
    }
}
