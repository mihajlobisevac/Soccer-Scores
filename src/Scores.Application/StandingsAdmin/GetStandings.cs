using Scores.Domain.Infrastructure;

namespace Scores.Application.StandingsAdmin
{
    [Service]
    public class GetStandings
    {
        private readonly IStandingsManager standingsManager;

        public GetStandings(IStandingsManager standingsManager)
        {
            this.standingsManager = standingsManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public int TeamCount { get; set; }
            public bool Deactivated { get; set; }
            public int TournamentId { get; set; }
        }

        public Response Do(int id) =>
            standingsManager.GetStandingsById(id, x => new Response
            {
                Id = x.Id,
                TeamCount = x.TeamCount,
                Deactivated = x.Deactivated,
                TournamentId = x.TournamentId,
            });
    }
}
