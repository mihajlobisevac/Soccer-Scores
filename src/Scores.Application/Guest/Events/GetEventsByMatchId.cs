using Scores.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scores.Application.Guest.Events
{
    [Service]
    public class GetEventsByMatchId
    {
        private readonly IEventManager eventManager;

        public GetEventsByMatchId(IEventManager eventManager)
        {
            this.eventManager = eventManager;
        }

        public class Response
        {
            public int HomeScore { get; set; }
            public int AwayScore { get; set; }
            public int Minute { get; set; }
            public bool IsHome { get; set; }
            public string Type { get; set; }
            public string Class { get; set; }
            public int PlayerAId { get; set; }
            public int PlayerBId { get; set; }
        }

        public IEnumerable<Response> Do(int matchId) =>
            eventManager.GetEventsByMatchId(matchId, x => new Response
            {
                HomeScore = x.HomeScore,
                AwayScore = x.AwayScore,
                Minute = x.Minute,
                IsHome = x.IsHome,
                Type = x.Type,
                Class = x.Class,
                PlayerAId = x.PlayerAId,
                PlayerBId = x.PlayerBId,
            });
    }
}
