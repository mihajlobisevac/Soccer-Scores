using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.Guest.Standings
{
    [Service]
    public class GetStandingsByTournamentId
    {
        private readonly IStandingsManager standingsManager;

        public GetStandingsByTournamentId(IStandingsManager standingsManager)
        {
            this.standingsManager = standingsManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public DateTime SeasonStart { get; set; }
            public DateTime SeasonEnd { get; set; }
        }

        public Response Do(int id)
            => standingsManager.GetStandingsByTournamentId(id, x => 
                new Response
                {
                    Id = x.Id,
                    SeasonStart = x.SeasonStart,
                    SeasonEnd = x.SeasonEnd,
                });
    }
}
