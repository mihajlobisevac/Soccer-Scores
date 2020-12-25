using Scores.Domain.Infrastructure;
using Scores.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scores.Application.StandingsAdmin
{
    [Service]
    public class CreateStandings
    {
        private readonly IStandingsManager standingsManager;

        public CreateStandings(IStandingsManager standingsManager)
        {
            this.standingsManager = standingsManager;
        }

        public class Request
        {
            public int TeamCount { get; set; }
            public bool Deactivated { get; set; }
            public int TournamentId { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public int TeamCount { get; set; }
            public bool Deactivated { get; set; }
            public int TournamentId { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var standings = new Standings
            {
                TeamCount = request.TeamCount,
                Deactivated = request.Deactivated,
                TournamentId = request.TournamentId
            };

            var result = await standingsManager.CreateStandings(standings);

            if (result <= 0)
                throw new Exception("Failed to create standings");

            return new Response
            {
                Id = standings.Id,
                TeamCount = standings.TeamCount,
                Deactivated = standings.Deactivated,
                TournamentId = standings.TournamentId,
            };
        }
    }
}
