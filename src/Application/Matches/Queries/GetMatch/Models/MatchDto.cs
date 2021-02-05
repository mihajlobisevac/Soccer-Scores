using AutoMapper;
using SoccerScores.Application.Common.Mappings;
using SoccerScores.Domain.Entities;
using System;
using System.Collections.Generic;

namespace SoccerScores.Application.Matches.Queries.GetMatch.Models
{
    public class MatchDto : IMapFrom<Match>
    {
        public int Id { get; set; }

        public DateTime KickOff { get; set; }
        public int GameWeek { get; set; }

        public ClubVm HomeTeam { get; set; }
        public ClubVm AwayTeam { get; set; }
        public SeasonVm Season { get; set; }

        public ICollection<IncidentVm> Incidents { get; set; }
        public ICollection<MatchPlayerVm> Players { get; set; }
    }
}
