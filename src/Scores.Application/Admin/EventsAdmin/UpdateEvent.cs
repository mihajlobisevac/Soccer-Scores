using Scores.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Scores.Application.EventsAdmin
{
    [Service]
    public class UpdateEvent
    {
        private readonly IEventManager eventManager;

        public UpdateEvent(IEventManager eventManager)
        {
            this.eventManager = eventManager;
        }

        public class Request
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

        public async Task<Response> Do(Request request)
        {
            var evnt = eventManager.GetEventById(request.Id, x => x);

            evnt.HomeScore = request.HomeScore;
            evnt.AwayScore = request.AwayScore;
            evnt.Minute = request.Minute;
            evnt.IsHome = request.IsHome;
            evnt.Type = request.Type;
            evnt.Class = request.Class;
            evnt.MatchId = request.MatchId;
            evnt.PlayerAId = request.PlayerAId;
            evnt.PlayerBId = request.PlayerBId;

            await eventManager.UpdateEvent(evnt);

            return new Response
            {
                Id = evnt.Id,
                HomeScore = evnt.HomeScore,
                AwayScore = evnt.AwayScore,
                Minute = evnt.Minute,
                IsHome = evnt.IsHome,
                Type = evnt.Type,
                Class = evnt.Class,
                MatchId = evnt.MatchId,
                PlayerAId = evnt.PlayerAId,
                PlayerBId = evnt.PlayerBId,
            };
        }
    }
}
