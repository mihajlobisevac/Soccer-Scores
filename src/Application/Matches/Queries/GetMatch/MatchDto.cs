using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SoccerScores.Application.Matches.Queries.GetMatch
{
    public class MatchDto : IMapFrom<Match>
    {
        public int Id { get; set; }

        public DateTime KickOff { get; set; }
        public int GameWeek { get; set; }

        public ClubViewModel HomeTeam { get; set; }
        public ClubViewModel AwayTeam { get; set; }
        public SeasonViewModel Season { get; set; }

        public ICollection<IncidentViewModel> Incidents { get; set; }
        public ICollection<MatchPlayerViewModel> Players { get; set; }
    }
}
