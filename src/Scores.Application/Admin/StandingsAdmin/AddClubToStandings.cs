using Scores.Domain.Infrastructure;
using Scores.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Scores.Application.StandingsAdmin
{
    [Service]
    public class AddClubToStandings
    {
        private readonly IStandingsManager standingsManager;

        public AddClubToStandings(IStandingsManager standingsManager)
        {
            this.standingsManager = standingsManager;
        }

        public class Request
        {
            public int ClubId { get; set; }
            public int StandingsId { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public int ClubId { get; set; }
            public int StandingsId { get; set; }
        }

        public async Task<Response> Do(Request request)
        {
            var clubStandings = new ClubStandings
            {
                StandingsId = request.StandingsId,
                ClubId = request.ClubId,
            };

            var result = await standingsManager.AddClub(clubStandings);

            if (result <= 0)
                throw new Exception("Failed to create clubStandings");

            return new Response
            {
                Id = clubStandings.Id,
                ClubId = clubStandings.ClubId,
                StandingsId = clubStandings.StandingsId,
            };
        }
    }
}
