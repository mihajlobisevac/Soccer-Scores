using Scores.Domain.Infrastructure;
using Scores.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Scores.Application.MatchesAdmin
{
    [Service]
    public class CreateMatch
    {
        private readonly IMatchManager matchManager;

        public CreateMatch(IMatchManager matchManager)
        {
            this.matchManager = matchManager;
        }

        public class Request
        {
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
            var match = new Match
            {
                KickOff = request.KickOff,
                HomeTeamId = request.HomeTeamId,
                AwayTeamId = request.AwayTeamId,
            };

            var result = await matchManager.CreateMatch(match);

            if (result <= 0)
                throw new Exception("Failed to create match");

            return new Response
            {
                Id = match.Id,
                KickOff = request.KickOff,
                HomeTeamId = request.HomeTeamId,
                AwayTeamId = request.AwayTeamId,
            };
        }
    }
}
