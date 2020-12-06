using Scores.Domain.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Scores.Application.MatchesAdmin
{
    [Service]
    public class UpdateMatch
    {
        private readonly IMatchManager matchManager;

        public UpdateMatch(IMatchManager matchManager)
        {
            this.matchManager = matchManager;
        }

        public class Request
        {
            public int Id { get; set; }
            public DateTime KickOff { get; set; }
            public int HomeTeamId { get; set; }
            public int AwayTeamId { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public DateTime KickOff { get; set; }
            public int HomeTeamId { get; set; }
            public int AwayTeamId { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var match = matchManager.GetMatchById(request.Id, x => x);

            match.KickOff = request.KickOff;
            match.HomeTeamId = request.HomeTeamId;
            match.AwayTeamId = request.AwayTeamId;

            await matchManager.UpdateMatch(match);

            return new Response
            {
                Id = match.Id,
                KickOff = match.KickOff,
                HomeTeamId = match.HomeTeamId,
                AwayTeamId = match.AwayTeamId,
            };
        }
    }
}
