using Scores.Domain.Infrastructure;

namespace Scores.Application.EventsAdmin
{
    [Service]
    public class GetEvent
    {
        private readonly IEventManager eventManager;

        public GetEvent(IEventManager eventManager)
        {
            this.eventManager = eventManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public int HomeScore { get; set; }
            public int AwayScore { get; set; }
            public int Minute { get; set; }
            public bool IsHome { get; set; }
            public string Type { get; set; }
            public string Class { get; set; }

            public int MatchId { get; set; }
            public int PlayerAId { get; set; }
            public int PlayerBId { get; set; }
        }

        public Response Do(int id) =>
            eventManager.GetEventById(id, x => new Response
            {
                Id = x.Id,
                HomeScore = x.HomeScore,
                AwayScore = x.AwayScore,
                Minute = x.Minute,
                IsHome = x.IsHome,
                Type = x.Type,
                Class = x.Class,
                MatchId = x.MatchId,
                PlayerAId = x.PlayerAId,
                PlayerBId = x.PlayerBId,
            });
    }
}
