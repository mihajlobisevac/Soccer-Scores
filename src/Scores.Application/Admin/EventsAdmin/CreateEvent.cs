using Scores.Domain.Infrastructure;
using Scores.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Scores.Application.EventsAdmin
{
    [Service]
    public class CreateEvent
    {
        private readonly IEventManager eventManager;

        public CreateEvent(IEventManager eventManager)
        {
            this.eventManager = eventManager;
        }

        public class Request
        {
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
            var evnt = new Incident
            {
                HomeScore = request.HomeScore,
                AwayScore = request.AwayScore,
                Minute = request.Minute,
                IsHome = request.IsHome,
                Type = request.Type,
                Class = request.Class,
                MatchId = request.MatchId,
                PlayerAId = request.PlayerAId,
                PlayerBId = request.PlayerBId,
            };

            var result = await eventManager.CreateEvent(evnt);

            if (result <= 0)
                throw new Exception("Failed to create event");

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
